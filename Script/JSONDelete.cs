using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

public class JSONDelete : MonoBehaviour
{

    //�ڑ�����URL
    private string URL = "http://192.168.56.56/api/deleteChat/";
    private const string URL2 = "http://192.168.56.56/api/user/";

    public InputField inputField;

    private string user1;


    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
    }

    public void OnClick()
    {
        //�R���[�`�����Ăяo��
        StartCoroutine("OnSend", URL);
    }

    //�R���[�`��
    IEnumerator OnSend(string url)
    {

        yield return OnSend2(URL2);

        string token = Login.GetToken();
        Debug.Log(token);

        WWWForm form = new WWWForm();
        form.AddField("id", inputField.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + token);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(user1);
            var request = new UnityWebRequest(url, "POST");
            using (request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData))
            {
                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.Send();
            }
            //UnityWebRequest�Ƀo�b�t�@���Z�b�g
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //URL�ɐڑ����Č��ʂ��߂��Ă���܂őҋ@
            yield return webRequest.SendWebRequest();

            //�G���[���o�Ă��Ȃ����`�F�b�N
            if (webRequest.isNetworkError)
            {
                //�ʐM���s
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("�폜");
                Debug.Log(webRequest.downloadHandler.text);

                //�ʐM����
                /*
                Debug.Log(webRequest.downloadHandler.text);
                JSON json = JsonUtility.FromJson<JSON>("{\"jsons\":" + webRequest.downloadHandler.text + "}");
                testResult.text = string.Format("�v�f��:{0}", json.jsons.Length);
                string test = "";
                foreach (JSONResult js in json.jsons)
                {
                    test += string.Format("{0}\n{1}:{2}\n", js.created_at, js.name, js.post);
                }
                textResult.text = test;
                */
            }
        }
    }
    IEnumerator OnSend2(string url)
    {
        string token = Login.GetToken();
        Debug.Log(token);


        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                //�ʐM���s
                Debug.Log(www.error);
            }
            else
            {
                //�ʐM����

                Debug.Log(www.downloadHandler.text);
                User user = JsonUtility.FromJson<User>("{\"users\":[" + www.downloadHandler.text + "]}");
                user1 = JsonUtility.ToJson(user);
                string test = "";
            }
        }
    }
}
