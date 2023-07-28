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

    //�I�u�W�F�N�g�֌W
    [SerializeField]
    public Text chatRoomText;

    void Start()
    {
        Debug.Log("Start�J�n");
        GetChatRommList(URL);
        Debug.Log("Start�I��");
    }

    public async Task GetChatRommList(string url)
    {
        Debug.Log("GetChatRoomList�J�n");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            await webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                //�ʐM���s
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("text:" + webRequest.downloadHandler.text);
                //�ʐM����
                var response = JsonUtility.FromJson<ChatList>("{\"chatLists\":" + webRequest.downloadHandler.text + "}");
                foreach (ChatListResult clr in response.chatLists)
                {
                    chatRoomText.text += string.Format("({0}){1}:{2}\n", clr.id, clr.chat_room_name, clr.created_at);
                }
            }
        }
        Debug.Log("GetChatRoomList�I��");
    }
}