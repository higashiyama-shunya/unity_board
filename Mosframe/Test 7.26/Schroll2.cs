using Mosframe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Schroll2 : MonoBehaviour, IDynamicScrollViewItem
{
    //フィールド

    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
    };

    public Text title;
    public Image background;

    public List<ChatListResult> list;

    //DynamicScrollViewを使用するための変数
    //オブジェクトから参照している値
    [SerializeField]
    public DynamicScrollView scrollView;

    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            list.Add(new ChatListResult { id = i, chat_room_name = "Room" + i });
            scrollView.totalItemCount = list.Count;
        }
        //Debug.Log(scrollView.totalItemCount);
        //scrollView.totalItemCount = list.Count;
        Debug.Log("totalitemCount:" + scrollView.totalItemCount + "\n listCount:" + list.Count);
    }

    public void onUpdateItem(int index)
    {
        Debug.Log("onUpdateItem開始:" + index);
        if (list.Count > index)
        {
            this.updateItem();
        }
        Debug.Log("onUpdateItem終了:" + index);
    }

    private void updateItem()
    {
        /*
        var data = realTimeSchroll.chatRoomList[this.dataIndex];

        this.background.color = this.colors[Mathf.Abs(this.dataIndex) % this.colors.Length];

        this.title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);
        */
    }



}
