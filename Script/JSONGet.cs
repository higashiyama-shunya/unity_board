using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

public class JSONGet : MonoBehaviour
{
    //接続するURL
    private const string URL = "http://192.168.56.56/api/getChat/";
    private const string URL2 = "http://192.168.56.56/api/user/";
    //JSONデータを表示するUI > Textオブジェクト
    public Text textResult;
    public Text testResult;
    public Text nameText;
    public InputField inputField;
    public static string id;


    private string user1;

    private int room_id;

    //ダミーデータ
    //string path = "Assets/Data/test2.json";


    // Start is called before the first frame update
    void Start()
    {

        //コルーチンを呼び出す
        //StartCoroutine("OnSend2", URL2);
        StartCoroutine("OnSend", URL);
    }

    public void OnClick()
    {
        //コルーチンを呼び出す
        StartCoroutine("OnSend", URL);
    }

    //コルーチン
    IEnumerator OnSend(string url)
    {
        yield return OnSend2(URL2);
        string token = Login.GetToken();

        room_id = ChatListGet.GetRoomId();

        Debug.Log(token);
        WWWForm form = new WWWForm();
        form.AddField("chat_room_id", room_id.ToString());
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + token);

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
                //ダミーデータ
                /*
                StreamReader reader = new StreamReader(path);
                string test2=reader.ReadToEnd();
                Debug.Log(test2);
                JSON json = JsonUtility.FromJson<JSON>("{\"jsons\":" + test2 + "}");
                testResult.text = string.Format("要素数:{0}", json.jsons.Length);
                string test = "";
                foreach (JSONResult js in json.jsons)
                {
                    test += string.Format("({0}){1}\n{2}:{3}\n", js.id,js.created_at, js.name, js.post);
                }
                textResult.text = test;
                */

                //通信成功

                Debug.Log(webRequest.downloadHandler.text);
                JSON json = JsonUtility.FromJson<JSON>("{\"jsons\":" + webRequest.downloadHandler.text + "}");
                testResult.text = string.Format("要素数:{0}", json.jsons.Length);
                string test = "";
                foreach (JSONResult js in json.jsons)
                {
                    test += string.Format("({0})\n{1}:{2}\n", js.id, js.created_at, js.chat);
                }
                textResult.text = test;
            }
        }
    }
    IEnumerator OnSend2(string url)
    {
        string token = Login.GetToken();
        Debug.Log(token);

        using (UnityWebRequest www = UnityWebRequest.Get("http://192.168.56.56/api/user/"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                //通信失敗
                Debug.Log(www.error);
            }
            else
            {
                //通信成功

                Debug.Log(www.downloadHandler.text);
                User user = JsonUtility.FromJson<User>("{\"users\":[" + www.downloadHandler.text + "]}");
                string test = "";
                user1 = JsonUtility.ToJson(user);
                foreach (UserResult us in user.users)
                {
                    test += string.Format("{0}さんようこそ！", us.name);
                    inputField.text = us.name;
                    id = us.id.ToString();
                }
                nameText.text = test;
            }
        }
    }
    public static string GetId()
    {
        return id;
    }
}