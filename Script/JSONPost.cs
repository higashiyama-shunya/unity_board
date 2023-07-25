using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class JSONPost : MonoBehaviour
{

    //�ڑ�����URL
    private const string URL = "http://192.168.56.56/api/addChat/";
    private const string URL2 = "http://192.168.56.56/api/user/";

    public InputField inputField;
    public InputField inputField2;
    public InputField inputField3;
    public Text text;
    public Text text2;
    public Text text3;

    public string user_id;

    private string user1;

    private int room_id;

    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
        inputField2 = inputField2.GetComponent<InputField>();
        text2 = text2.GetComponent<Text>();
        inputField3 = inputField3.GetComponent<InputField>();
        text3 = text3.GetComponent<Text>();

    }

    public void InputText()
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
        text.text = inputField.text;
        text2.text = inputField2.text;
        text3.text = inputField3.text;
    }

    public void OnClick()
    {
        //�R���[�`�����Ăяo��
        user_id = JSONGet.GetId();
        Debug.Log(user_id);
        InputText();
        StartCoroutine("OnSend", URL);
    }


    IEnumerator OnSend(string url)
    {
        yield return OnSend2(URL2);
        string token = Login.GetToken();

        room_id=ChatListGet.GetRoomId();

        //POST������
        WWWForm form = new WWWForm();
        //form.AddField("chat_name", inputField.text);
        form.AddField("chat_room_id", room_id.ToString());
        form.AddField("user_id", user_id);
        //form.AddField("user_id", user_id);
        form.AddField("chat", inputField2.text);

        //URL��POST�ŗp��
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form)) { 
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
                    user_id = us.id.ToString();
                }
            }
        }
    }
}
