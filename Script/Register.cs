using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    private const string URL = "http://192.168.56.56/api/register/";

    public InputField inputField;
    public InputField inputField2;
    public InputField inputField3;
    public Text text;
    public Text text2;
    public Text text3;

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
        //テキストにinputFieldの内容を反映
        text.text = inputField.text;
        text2.text = inputField2.text;
        text3.text = inputField3.text;
    }

    public void OnClick()
    {
        //コルーチンを呼び出す
        InputText();
        StartCoroutine("OnSend", URL);
        SceneManager.LoadScene("LoginScene");
    }


    IEnumerator OnSend(string url)
    {
        //POSTする情報
        WWWForm form = new WWWForm();
        form.AddField("email", inputField.text);
        form.AddField("name", inputField2.text);
        form.AddField("password", inputField3.text);

        //URLをPOSTで用意
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            //UnityWebRequestにバッファをセット
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //URLに接続して結果が戻ってくるまで待機
            yield return webRequest.SendWebRequest();

            //エラーが出ていないかチェック
            if (webRequest.isNetworkError)
            {
                //通信失敗
                Debug.Log(webRequest.error);
            }
            else
            {
                //通信成功
                Debug.Log(webRequest.downloadHandler.text);

                /*
                WWWForm loginForm = new WWWForm();
                form.AddField("email", inputField.text);
                form.AddField("password", inputField3.text);
                UnityWebRequest webRequest1=UnityWebRequest.Post("http://192.168.56.56/api/login/",loginForm);
                */
            }
        }
    }

}
