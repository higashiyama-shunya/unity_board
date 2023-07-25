using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mosframe;

public class RealTimeSchroll : MonoBehaviour
{

    //データを入れるリストの変数
    public List<ChatListResult> chatRoomList = new List<ChatListResult>();

    //DynamicScrollViewを使用するための変数
    public DynamicScrollView scrollView;


    void Awake()
    {
        Debug.Log("Awake開始");

        //this.insertItem(0, new ChatListResult { id = 0, chat_room_name = "value0", owner_id = 100 });
        for (int i = 1; i < 21; i++)
        {
            //chatRoomList.Add(new ChatListResult { id = i, chat_room_name = "room" + i, owner_id = 21 - i });
            this.insertItem(0, new ChatListResult { id = i, chat_room_name = "room" + i, owner_id = 21 - i });
        }
        Debug.Log("Awake終了");
    }

    void Start()
    {
        Debug.Log("Start開始");
        foreach (ChatListResult clr in chatRoomList)
        {
            //this.insertItem(0, clr);
            Debug.Log("id:" + clr.id + "\n name:" + clr.chat_room_name + "\n owner_id:" + clr.owner_id);

        }
        Debug.Log("Start終了");
    }

    public void insertItem(int index, ChatListResult data)
    {

        // set custom data

        this.chatRoomList.Insert(index, data);
        //Debug.Log(this.chatRoomList.Count);
        //Debug.Log(scrollView.totalItemCount);
        this.scrollView.totalItemCount = this.chatRoomList.Count;
        Debug.Log(scrollView.totalItemCount);
    }
}
