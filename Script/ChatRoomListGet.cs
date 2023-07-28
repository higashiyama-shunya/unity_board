using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEditor.Compilation;

public class ChatRoomListGet : MonoBehaviour
{
    //接続するURL
    private const string URL = "http://192.168.56.56/api/getAllChatRoom/";

    //オブジェクト関係
    [SerializeField]
    public Text chatRoomText;

    void Start()
    {
        Debug.Log("Start開始");
        GetChatRommList(URL);
        Debug.Log("Start終了");
    }

    public async Task GetChatRommList(string url)
    {
        Debug.Log("GetChatRoomList開始");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                //通信失敗
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("text:" + webRequest.downloadHandler.text);
                //通信成功
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                foreach (ChatListResult clr in response.chatLists)
                {
                    chatRoomText.text += string.Format("({0}){1}:{2}\n", clr.id, clr.chat_room_name, clr.created_at);
                }
            }
        }
        Debug.Log("GetChatRoomList終了");
    }
}