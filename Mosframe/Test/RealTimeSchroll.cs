using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mosframe;

public class RealTimeSchroll : MonoBehaviour
{

    //�f�[�^�����郊�X�g�̕ϐ�
    public List<ChatListResult> chatRoomList = new List<ChatListResult>();

    //DynamicScrollView���g�p���邽�߂̕ϐ�
    public DynamicScrollView scrollView;


    void Awake()
    {
        Debug.Log("Awake�J�n");

        //this.insertItem(0, new ChatListResult { id = 0, chat_room_name = "value0", owner_id = 100 });
        for (int i = 1; i < 21; i++)
        {
            //chatRoomList.Add(new ChatListResult { id = i, chat_room_name = "room" + i, owner_id = 21 - i });
            this.insertItem(0, new ChatListResult { id = i, chat_room_name = "room" + i, owner_id = 21 - i });
        }
        Debug.Log("Awake�I��");
    }

    void Start()
    {
        Debug.Log("Start�J�n");
        foreach (ChatListResult clr in chatRoomList)
        {
            //this.insertItem(0, clr);
            Debug.Log("id:" + clr.id + "\n name:" + clr.chat_room_name + "\n owner_id:" + clr.owner_id);

        }
        Debug.Log("Start�I��");
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
