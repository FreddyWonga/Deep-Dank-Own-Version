using UnityEngine;

namespace Dungbeetle {
    public class BugReportFormLauncher : MonoBehaviour {
        public GUISkin guiSkin;

        public void OnGUI() {
            GUILayout.Label("F7 - Report Bug", guiSkin.label);
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.F7))
                BugReportForm.Launch(GetSceneName(), GetBuildName(), GetAttachments());
        }

        private AttachmentCollection GetAttachments() {
            var result = new AttachmentCollection();
            result.AddString("Example attachment (could be a saved game, settings etc.).");
            result.AddString("Yet another example attachment...");
            return result;
        }

        private string GetBuildName() {
            return BuildNameArk.BuildName;
        }

        private string GetSceneName() {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }
}