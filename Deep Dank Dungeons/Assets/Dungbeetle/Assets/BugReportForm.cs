using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

namespace Dungbeetle {
    public class BugReportForm : MonoBehaviour {

        private static BugReportForm instance;

        public static void Launch(string scene_name, string build_name, AttachmentCollection _attachments) {
            if (instance == null) {
                instance = new GameObject("[BugReportForm]").AddComponent<BugReportForm>();
                instance.launched = true;
                instance.screenshotSource = new ScreenshotSource();
                instance.email = EditorReporterConfig.DefaultReporter ?? "";
                instance.bugDescription = "";
                instance.sceneName      = scene_name;
                instance.buildName      = build_name;
                instance.attachments    = _attachments;
            }
        }

        private static Thread mainThread;

        private IEnumerator Start() {
            mainThread = Thread.CurrentThread;
            if (!launched) {
                Debug.LogWarning("BugReportForm should only be created by BugReportForm.Launch(...)", this);
                yield break;
            }

            yield return screenshotSource.Wait(15f); // Wait for screenshot to be written to file, or time out.
            if (screenshotSource.Ready)
                yield return attachments.Wait(); // Wait for log to be read from file (don't time out, the length is capped anyway).

            if (!screenshotSource.Ready)
                CloseWithMessage(false, "The screenshot couldn't be read from file.", false);
            else if (!attachments.Ready)
                CloseWithMessage(false, "The attachments couldn't be read from file.", false);

            showing = true;
        }

        private bool launched;

        private ScreenshotSource screenshotSource;
        private string email;
        private string bugDescription;
        private string sceneName;
        private string buildName;
        private AttachmentCollection attachments;
        private bool fullAttachments;

        private bool showing;
        private GUIKeyPress returnKeyPress = new GUIKeyPress(KeyCode.Return);
        private TimeoutMessage errorMessage;
        private LayoutMessage progressMessage;
        private LayoutMessage closingMessage;

        private void CloseWithMessage(bool isServerResponse, string message, bool waitForLayout = true) {
            string result;
            if (isServerResponse) {
                if (message.Equals("Accepted"))
                    result = "Report received.\nThank you!";
                else
                    result = string.Format("Server: {0}", message);
            }
            else {
                result = string.Format("Error: {0}", message);
            }
            closingMessage = new LayoutMessage(result, !waitForLayout);
        }

        private void Dispose() {
            if (screenshotSource != null)
                screenshotSource.Dispose();

            screenshotSource = null;
            instance = null;

            Destroy(gameObject);
        }

        public void OnGUI() {
            if (!showing)
                return;

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape) {
                Dispose();
                return;
            }

            // Use the KeyDown and KeyUp events to get exactly one submission per press of the return button.
            bool returnThisFrame = returnKeyPress.ProcessEvent(Event.current);
            bool ctrlReturnThisFrame = returnThisFrame && Event.current.control;

            if (progressMessage != null && progressMessage.Ready) {
                GUI.depth = 1;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.Box("", GUILayout.Height(Screen.height), GUILayout.Width(Screen.width));
                GUILayout.EndArea();

                GUI.depth = 0;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.FlexibleSpace();
                GUILayout.Label(progressMessage, centeredTextStyle);
                GUILayout.Space(20f);

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Cancel", GUILayout.Width(150f))) {
                    progressMessage.Cancel();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.EndArea();
            }
            else if (closingMessage != null && closingMessage.Ready) {
                GUI.depth = 1;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.Box("", GUILayout.Height(Screen.height), GUILayout.Width(Screen.width));
                GUILayout.EndArea();

                GUI.depth = 0;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.FlexibleSpace();
                GUILayout.Label(closingMessage, centeredTextStyle);
                GUILayout.Space(20f);

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Ok", GUILayout.Width(150f)) || returnThisFrame)
                    Dispose();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.EndArea();
            }
            else {
                GUI.depth = 1;

                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.Box("", GUILayout.Height(Screen.height), GUILayout.Width(Screen.width));
                GUILayout.EndArea();

                GUI.depth = 0;
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Screen.width / 4f);

                if (errorMessage == null || errorMessage.EndReached)
                    GUILayout.Box(screenshotSource.FullTexture, GUILayout.Height(Screen.height / 2f), GUILayout.Width(Screen.width / 2f));
                else
                    GUILayout.Label(errorMessage, centeredTextStyle, GUILayout.Height(Screen.height / 2f), GUILayout.Width(Screen.width / 2f));

                GUILayout.EndHorizontal();

                var email_rect             = GUILayoutUtility.GetRect(Screen.width, 24f);
                var email_text_rect        = GUILayoutUtility.GetRect(Screen.width, 24f);
                var description_rect       = GUILayoutUtility.GetRect(Screen.width, 24f);
                var short_attachments_rect = GUILayoutUtility.GetRect(Screen.width, 24f);
                var description_text_rect  = GUILayoutUtility.GetRect(Screen.width, Screen.height - (126f + Screen.height / 2f));

                GUILayout.BeginHorizontal();
                var cancelButton_rect = GUILayoutUtility.GetRect(Screen.width / 2f, 24f);
                var sendButton_rect   = GUILayoutUtility.GetRect(Screen.width / 2f, 24f);
                GUILayout.EndHorizontal();

                { // This implements CTRL+A support in the game view, which is normally disabled outside of editor windows.
                    var e = Event.current;
                    if (e.type == EventType.ValidateCommand && e.commandName == "SelectAll") {
                        var emailId       = GUIUtility.GetControlID(FocusType.Keyboard,       email_text_rect);
                        var descriptionId = GUIUtility.GetControlID(FocusType.Keyboard, description_text_rect);
                        if (GUIUtility.keyboardControl == descriptionId) {
                            TextEditor textEditor = (TextEditor) GUIUtility.GetStateObject(typeof(TextEditor), descriptionId);
                            textEditor.SelectAll();
                            e.Use();
                        } else if (GUIUtility.keyboardControl == emailId) {
                            TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), emailId);
                            textEditor.SelectAll();
                            e.Use();
                        }
                    }
                }

                                    GUI.Label    (email_rect,            "Email (if you allow us to contact you)");
                email =             GUI.TextField(email_text_rect,       email);
                                    GUI.Label   (description_rect,       "Description of bug *");
                fullAttachments   = GUI.Toggle  (short_attachments_rect, fullAttachments, "Send full log");
                bugDescription    = GUI.TextArea(description_text_rect,  bugDescription);
                var cancelPressed = GUI.Button  (cancelButton_rect,      "Cancel");
                var sendPressed   = GUI.Button  (sendButton_rect,        "Send Bugreport");

                if (sendPressed || ctrlReturnThisFrame) {
                    EditorReporterConfig.DefaultReporter = email;

                    attachments.AddLog(!fullAttachments);
                    var e = attachments.Wait();
                    while (e.MoveNext())
                        ; // Wait

                    progressMessage = new LayoutMessage("Uploading: 0%", true);
                    BugReportSender.Send(screenshotSource.Bytes, bugDescription, email, sceneName, buildName, HandleProgressMessage, HandleBugProgress, HandleAttachmentProgress, progressMessage.CancellationToken, PostSendResult, attachments.ToStringArray());
                }
                if (cancelPressed)
                    Dispose();
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
        }

        private void HandleProgressMessage(string value, bool socketFailed, string socketFailMsg) {
            if (progressMessage != null && !SocketFailure(socketFailed, socketFailMsg))
                progressMessage.content = value;
        }

        private void HandleBugProgress(float value, bool socketFailed, string socketFailMsg) {
            if (progressMessage != null && !SocketFailure(socketFailed, socketFailMsg))
                progressMessage.content = string.Format("Uploading report: {0}%", (int) (value * 100f));
        }

        private void HandleAttachmentProgress(float value, bool socketFailed, string socketFailMsg) {
            if (progressMessage != null && !SocketFailure(socketFailed, socketFailMsg))
                progressMessage.content = string.Format("Uploading attachments: {0}%", (int) (value * 100f));
        }

        private void PostSendResult(string resultMessage, bool socketFailed, string socketFailMsg) {
            if (!SocketFailure(socketFailed, socketFailMsg)) {
                CloseWithMessage(true, resultMessage);
                progressMessage = null;
            }
        }

        private bool SocketFailure(bool socketFailed, string socketFailMsg) {
            if (socketFailed) {
                errorMessage = "Connection failed";
                Debug.LogErrorFormat("Reporter connection failed\n{0}", socketFailMsg);
                progressMessage = null;
            }
            return socketFailed;
        }

        private class LayoutMessage {
            public string content;
            private bool ready;

            private CancellationTokenSource source;
            private CancellationToken       token;

            public CancellationToken CancellationToken => token;

            public void Cancel() {
                source.Cancel();
            }

            public LayoutMessage(string content, bool ready) {
                this.source  = new CancellationTokenSource();
                this.token   = source.Token;
                this.content = content;
                this.ready   = ready;
            }
            public bool Ready { get { return ready || (ready = UnityEngine.Event.current.type == EventType.Layout); } }
            public static implicit operator string(LayoutMessage m) {
                return m.content;
            }
        }

        private class TimeoutMessage {
            private string content;
            private float endTime;
            private bool endTimeInitialized;
            private TimeoutMessage(string content) {
                this.content = content;
                if (mainThread.Equals(Thread.CurrentThread)) {
                    endTime = Time.realtimeSinceStartup + 3f;
                    endTimeInitialized = true;
                }
                else {
                    endTime = float.PositiveInfinity;
                }
            }
            public bool EndReached {
                get {
                    if (endTimeInitialized) {
                        return Time.realtimeSinceStartup > endTime;
                    } else if (mainThread.Equals(Thread.CurrentThread)) {
                        endTime = Time.realtimeSinceStartup + 3f;
                        endTimeInitialized = true;
                    }
                    return false;
                }
            }
            public static implicit operator string(TimeoutMessage m) {
                return m.content;
            }
            public static implicit operator TimeoutMessage(string m) {
                return new TimeoutMessage(m);
            }
        }

        private GUIStyle centeredTextStyle { get { return _centeredTextStyle ?? (_centeredTextStyle = CreateCenteredTextStyle()); } }
        private GUIStyle _centeredTextStyle;

        private GUIStyle CreateCenteredTextStyle() {
            var result = new GUIStyle(GUI.skin.label);
            result.normal.textColor = Color.white;
            result.alignment = TextAnchor.MiddleCenter;
            return result;
        }
    }

    public class GUIKeyPress {
        private bool keyDown;
        private KeyCode keyCode;

        public GUIKeyPress(KeyCode keyCode) { this.keyCode = keyCode; }

        public bool ProcessEvent(Event currentEvent) {
            // Use the KeyDown and KeyUp events to get exactly one submission per press of the given key.
            if (!keyDown) {
                if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == keyCode) {
                    currentEvent.Use();
                    return (keyDown = true);
                }
            } else if (currentEvent.type == EventType.KeyUp && currentEvent.keyCode == keyCode) {
                keyDown = false;
                currentEvent.Use();
            }
            return false;
        }
    }
}
