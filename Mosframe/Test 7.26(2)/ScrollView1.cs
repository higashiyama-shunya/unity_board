using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView1 : MonoBehaviour
{

    public List<ChatListResult> list = new List<ChatListResult>();

    [SerializeField]
    DynamicScrollView dynamicScrollView;

    private void Start()
    {
        for (int i = 1; i < 6; i++)
        {
            list.Add(new ChatListResult() { id = i, chat_room_name = "room" + i, owner_id = i });
        }
        dynamicScrollView.totalItemCount = list.Count;
    }
}
