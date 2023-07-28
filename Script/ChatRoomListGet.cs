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

    //オブジェクト関係  objectを探すようにする。
    /*
    [SerializeField]
    public Text chatRoomText;
    */
    async Task Start()
    {
        Debug.Log("Start開始");
        await TextDisplay();
        Debug.Log("Start終了");
    }

    public async Task<ChatList> GetChatRommList(string url)
    {
        Debug.Log("GetChatRoomList開始");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                //通信失敗
                Debug.Log(webRequest.error);
                Debug.Log("GetChatRoomList終了");
                return null;
            }
            else
            {
                Debug.Log("text:" + webRequest.downloadHandler.text);
                //通信成功
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                Debug.Log("GetChatRoomList終了");
                return response;
            }
        }
    }

    public async Task TextDisplay()
    {
        Debug.Log("TextDisplay開始");
        Text text;
        GameObject obj = transform.Find("ChatRoomListText").gameObject;
        text = obj.GetComponent<Text>();
        text.text = "";

        var request = await GetChatRommList(URL);
        foreach (ChatListResult clr in request.chatLists)
        {
            text.text += string.Format("({0}){1}:{2}\n", clr.id, clr.chat_room_name, clr.created_at);
        }

        Debug.Log(request.chatLists[0].chat_room_name);
        Debug.Log("TextDisplay終了");
    }
}