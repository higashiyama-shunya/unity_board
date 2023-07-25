using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class JoinChatRoom : MonoBehaviour
{
    //�ڑ�����URL
    private const string URL = "http://192.168.56.56/api/joinChatRoom";
    private const string URL2 = "http://192.168.56.56/api/user/";

    //�ϐ�
    public static int id;
    public InputField inputField;
    private string user1;

    public void OnClick()
    {
        StartCoroutine("OnSend", URL);
    }

    //�R���[�`��
    IEnumerator OnSend(string url)
    {
        yield return OnSend2(URL2);
        string token = Login.GetToken();
        Debug.Log(token);

        WWWForm form = new WWWForm();
        form.AddField("chat_room_id", int.Parse(inputField.text));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            Debug.Log(user1);
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
                //�ʐM����

                Debug.Log(webRequest.downloadHandler.text);
                /*ChatList chatList = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                string test = "";
                foreach (ChatListResult cr in chatList.chatLists)
                {

                    test += string.Format("({0}){1}:{2}\n", cr.id, cr.chat_room_name, cr.created_at);

                }
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
                foreach (UserResult us in user.users)
                {
                    id = us.id;
                }
            }
        }
    }
}
