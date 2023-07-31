using Mosframe;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ScrollView2 : MonoBehaviour
{
    private const string URL = "http://192.168.56.56/api/getAllChatRoom/";

    public List<ChatListResult> list = new List<ChatListResult>();

    async Task Start()
    {
        Debug.Log("Start開始");
        await ListCreate();
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
                //接続失敗
                Debug.Log(webRequest.error);
                Debug.Log("GetChatRoomList終了");
                return null;
            }
            else
            {
                //接続成功
                Debug.Log("text:" + webRequest.downloadHandler.text);
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                Debug.Log("GetChatRoomList終了");
                return response;
            }
        }
    }

    public async Task ListCreate()
    {
        DynamicScrollView dynamicScrollView = gameObject.GetComponent<DynamicScrollView>();
        var request = await GetChatRommList(URL);
        foreach (ChatListResult clr in request.chatLists)
        {
            list.Add(new ChatListResult() { id = clr.id, chat_room_name = clr.chat_room_name, owner_id = clr.owner_id });
        }
        Debug.Log("変更前:" + dynamicScrollView.totalItemCount + ":" + list.Count);
        dynamicScrollView.totalItemCount = list.Count;
        Debug.Log("変更後:" + dynamicScrollView.totalItemCount + ":" + list.Count);
        Debug.Log(request.chatLists[0].chat_room_name);
        Debug.Log("TextDisplay終了");
    }
}
