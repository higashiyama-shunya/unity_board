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
    //�ڑ�����URL
    private const string URL = "http://192.168.56.56/api/getAllChatRoom/";

    //�I�u�W�F�N�g�֌W  object��T���悤�ɂ���B
    /*
    [SerializeField]
    public Text chatRoomText;
    */
    async Task Start()
    {
        Debug.Log("Start�J�n");
        await TextDisplay();
        Debug.Log("Start�I��");
    }

    public async Task<ChatList> GetChatRommList(string url)
    {
        Debug.Log("GetChatRoomList�J�n");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                //�ʐM���s
                Debug.Log(webRequest.error);
                Debug.Log("GetChatRoomList�I��");
                return null;
            }
            else
            {
                Debug.Log("text:" + webRequest.downloadHandler.text);
                //�ʐM����
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                Debug.Log("GetChatRoomList�I��");
                return response;
            }
        }
    }

    public async Task TextDisplay()
    {
        Debug.Log("TextDisplay�J�n");
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
        Debug.Log("TextDisplay�I��");
    }
}