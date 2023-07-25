using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5)]
public class ChatListGet : MonoBehaviour
{
    //接続するURL
    private const string URL = "http://192.168.56.56/api/joinChatRoom/";
    private const string URL2 = "http://192.168.56.56/api/user/";
    private const string URL3 = "http://192.168.56.56/api/getAllChatRoom/";
    //JSONデータを表示するUI > Textオブジェクト
    public Text textResult;
    public Text testResult;
    public Text nameText;
    public static int id;
    public List<int> list;

    public static int room_id;
    public InputField inputField;

    public GameObject board;
    public Text cloneText;


    public static ChatList chatList;

    //ダミーデータ
    //string path = "Assets/Data/test2.json";

    //テストユーザー作成(ログイン用)

    //テストURL
    private const string URL4 = "http://192.168.56.56/api/login/";
    private string email = "sato@example.com";
    private string pass = "higashiyama";

    private string userToken;




    // Start is called before the first frame update
    public void Awake()
    {

        //コルーチンを呼び出す
        //StartCoroutine("OnSend2", URL2);
        //StartCoroutine("OnSend3", URL3);
        StartCoroutine("OnSend", URL3);
    }

    public void OnClick()
    {
        //コルーチンを呼び出す
        room_id = int.Parse(inputField.text);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClick2()
    {
        StartCoroutine("OnSend", URL3);
    }

    public static int GetRoomId()
    {
        return room_id;
    }

    public static ChatList GetChatList()
    {
        return chatList;
    }

    //コルーチン
    IEnumerator OnSend(string url)
    {
        yield return OnSend2(URL2);
        //yield return OnSend3(URL3);
        string token = userToken;
        //Debug.Log(token);

        //WWWForm form = new WWWForm();
        //form.AddField("user_id", id);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
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
                //通信成功

                //Debug.Log(webRequest.downloadHandler.text);
                chatList = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                //Debug.Log(chatList.chatLists[0].chat_room_name);
                string test = "";
                foreach (ChatListResult cr in chatList.chatLists)
                {
                    test += string.Format("({0}){1}:{2}\n", cr.id, cr.chat_room_name, cr.created_at);
                }
                textResult.text = test;
            }
        }
    }
    /*
    IEnumerator OnSendtest(string url)
    {
        yield return OnSend2(URL2);
        //yield return OnSend3(URL3);
        string token = Login.GetToken();
        Debug.Log(token);

        //WWWForm form = new WWWForm();
        //form.AddField("user_id", id);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
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
                //通信成功

                Debug.Log(webRequest.downloadHandler.text);
                ChatList chatList = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                string test = "";
                foreach (ChatListResult cr in chatList.chatLists)
                {
                    test += string.Format("({0}){1}:{2}\n", cr.id, cr.chat_room_name, cr.created_at);
                }
                textResult.text = test;

            }
        }
    }
    */

    IEnumerator OnSend2(string url)
    {
        yield return OnSend4(URL4);
        //テスト用
        string token = userToken;
        //string token = Login.GetToken();
        //Debug.Log(token);


        using (UnityWebRequest www = UnityWebRequest.Get(url))
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

                //Debug.Log(www.downloadHandler.text);
                User user = JsonUtility.FromJson<User>("{\"users\":[" + www.downloadHandler.text + "]}");
                string test = "";
                foreach (UserResult us in user.users)
                {
                    test += string.Format("{0}さんようこそ！", us.name);
                    id = us.id;
                }
                nameText.text = test;
            }
        }
    }

    IEnumerator OnSend3(string url)
    {
        yield return OnSend2(URL2);
        string token = Login.GetToken();
        Debug.Log(token);

        WWWForm form = new WWWForm();
        form.AddField("user_id", id);
        form.AddField("chat_room_id", GetRoomId());

        //using (UnityWebRequest www = UnityWebRequest.Post(url,form))
        using (UnityWebRequest www = UnityWebRequest.Get(url))
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
                Match match = JsonUtility.FromJson<Match>("{\"matchs\":" + www.downloadHandler.text + "}");
                string test = "";
                int testid = GetId();
                foreach (MatchResult ms in match.matchs)
                {
                    if (testid == ms.user_id)
                    {
                        list.Add(ms.chat_room_id);
                        Debug.Log(ms.chat_room_id);
                    }
                }
            }
        }
    }

    //テスト用メソッド
    IEnumerator OnSend4(string url)
    {
        //POSTする情報
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", pass);

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
                //Debug.Log(webRequest.downloadHandler.text);

                Token token = JsonUtility.FromJson<Token>("{\"tokens\":[" + webRequest.downloadHandler.text + "]}");
                //testResult.text = string.Format("要素数:{0}", json.jsons.Length);
                string test = "";
                foreach (TokenResult js in token.tokens)
                {
                    //Debug.Log(js.token);
                    test += string.Format("{0}", js.token);
                }
                userToken = test;
            }
        }
    }


    public static int GetId()
    {
        return id;
    }
}
