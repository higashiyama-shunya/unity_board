using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private const string URL = "http://192.168.56.56/api/login/";

    public InputField inputField;
    public InputField inputField2;
    public Text text;
    public Text text2;

    [SerializeField]
    public static string userToken;


    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
        inputField2 = inputField2.GetComponent<InputField>();
        text2 = text2.GetComponent<Text>();
    }

    public void InputText()
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
        text.text = inputField.text;
        text2.text = inputField2.text;
    }

    public void OnClick()
    {
        //�R���[�`�����Ăяo��
        InputText();
        StartCoroutine("OnSend", URL);
    }


    IEnumerator OnSend(string url)
    {
        //POST������
        WWWForm form = new WWWForm();
        form.AddField("email", inputField.text);
        form.AddField("password", inputField2.text);

        //URL��POST�ŗp��
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
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

                Token token = JsonUtility.FromJson<Token>("{\"tokens\":[" + webRequest.downloadHandler.text + "]}");
                //testResult.text = string.Format("�v�f��:{0}", json.jsons.Length);
                string test = "";
                foreach (TokenResult js in token.tokens)
                {
                    Debug.Log(js.token);
                    test += string.Format("{0}", js.token);
                }
                userToken = test;
                SceneManager.LoadScene("ChatListScene");
            }
        }
    }
    public static string GetToken()
    {
        return userToken;
    }
}
