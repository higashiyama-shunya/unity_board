using Mosframe;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ScrollView1 : MonoBehaviour
{

    public List<ChatListResult> list = new List<ChatListResult>();

    private void Start()
    {
        DynamicScrollView dynamicScrollView;
        GameObject obj = transform.parent.gameObject;
        dynamicScrollView = obj.GetComponent<DynamicScrollView>();
        for (int i = 1; i < 20; i++)
        {
            list.Add(new ChatListResult() { id = i, chat_room_name = "room" + i, owner_id = i });
        }
        dynamicScrollView.totalItemCount = list.Count;
    }
}
