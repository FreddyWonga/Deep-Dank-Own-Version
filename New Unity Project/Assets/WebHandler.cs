using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebHandler : MonoBehaviour
{
    readonly string getUrl = "https://deep-dank-dungeons-scoreboard.000webhostapp.com/Get_Highscore.php";
    readonly string postUrl = "https://deep-dank-dungeons-scoreboard.000webhostapp.com/Post_Highscore.php";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(getUrl);

        yield return www.SendWebRequest();

        if (www.isNetworkError == true || www.isHttpError == true)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator PostScoreRequest(int scoreValue)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("scoreKey", scoreValue.ToString()));

        UnityWebRequest www = UnityWebRequest.Get(getUrl);

        yield return www.SendWebRequest();

        if (www.isNetworkError == true || www.isHttpError == true)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
