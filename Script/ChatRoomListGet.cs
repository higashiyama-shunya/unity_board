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

    //オブジェクト用→getComponentで取得するように変更
    /*
    [SerializeField]
    public Text chatRoomText;
    */

    //await でTextDisplayが終わってから次の処理に移行するようにしている。
    async Task Start()
    {
        Debug.Log("Start開始");
        await TextDisplay();
        Debug.Log("Start終了");
    }

    //戻り値はTask<ChatList>でChatListが戻り値になる。
    //API通信を行うメソッド
    public async Task<ChatList> GetChatRommList(string url)
    {
        Debug.Log("GetChatRoomList開始");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            //今まではコルーチンでyield return でやっていた→コルーチンではなくasync/awaitを使って非同期処理を行うように
            //await webRequest.SendWebRequestで出来る。※通常ではできなく、ライブラリや使えるようにするためのクラスを作成する必要がある。
            //webRequest.SendWebRequestが終わるまで次の処理に以降しないようにしている。
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
                Debug.Log("text:" + webRequest.downloadHandler.text);
                //接続成功
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                Debug.Log("GetChatRoomList終了");
                return response;
            }
        }
    }

    //Taskだと戻り値がないということになる。
    public async Task TextDisplay()
    {
        Debug.Log("TextDisplay開始");
        Text text;
        GameObject obj = transform.Find("ChatRoomListText").gameObject;
        text = obj.GetComponent<Text>();
        text.text = "";

        //API通信が終わるまで待つ処理。
        var request = await GetChatRommList(URL);
        foreach (ChatListResult clr in request.chatLists)
        {
            text.text += string.Format("({0}){1}:{2}\n", clr.id, clr.chat_room_name, clr.created_at);
        }

        Debug.Log(request.chatLists[0].chat_room_name);
        Debug.Log("TextDisplay終了");
    }
}