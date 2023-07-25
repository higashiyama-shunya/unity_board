using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class JSONEdit : MonoBehaviour
{

    //接続するURL
    private string URL = "http://192.168.56.56/api/chat_pages/";

    public InputField inputField;
    public InputField inputField2;
    public InputField inputField3;
    //public Text text;
    //public Text text2;
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        id = JSONUpdate.GetId().ToString();
        inputField = inputField.GetComponent<InputField>();
        //text = text.GetComponent<Text>();
        inputField2 = inputField2.GetComponent<InputField>();
        //text2 = text2.GetComponent<Text>();
        inputField3 = inputField3.GetComponent<InputField>();
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        //text.text = inputField.text;
        //text2.text = inputField2.text;

    }

    public void OnClick()
    {
        //コルーチンを呼び出す
        URL += id;
        //InputText();
        StartCoroutine("OnSend", URL);
        SceneManager.LoadScene("SampleScene");
    }


    IEnumerator OnSend(string url)
    {
        //POSTする情報
        WWWForm form = new WWWForm();
        form.AddField("chat_name", inputField.text);
        form.AddField("user_id", inputField3.text);
        form.AddField("chat", inputField2.text);

        //URLをPOSTで用意
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        //UnityWebRequestにバッファをセット
        webRequest.method = "Put";
        yield return webRequest.SendWebRequest();
        //URLに接続して結果が戻ってくるまで待機

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
        }
    }
}
