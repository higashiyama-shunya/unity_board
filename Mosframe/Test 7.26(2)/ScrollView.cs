using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    private static List<ChatListResult> list = new List<ChatListResult>();

    [SerializeField]
    DynamicScrollView dynamicScrollView;

    public static List<ChatListResult> getChatListResultList()
    {
        return list;
    }

    private void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            list.Add(new ChatListResult() { id = i, chat_room_name = "room" + i, owner_id = i });
        }
        dynamicScrollView.totalItemCount = list.Count;
    }
}
