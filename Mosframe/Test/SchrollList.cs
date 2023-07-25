using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SchrollList : UIBehaviour, IDynamicScrollViewItem
{
    //�t�B�[���h
    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
    };

    public Text title;
    public Image background;

    public RealTimeSchroll realTimeSchroll;

    private int dataIndex = -1;
    /*
    protected override void OnEnable()
    {
        Debug.Log("onEnable�J�n");
        base.OnEnable();
        Debug.Log("onEnable�I��");
    }

    protected override void OnDisable()
    {
        Debug.Log("onDisable�J�n");
        base.OnDisable();
        Debug.Log("onDisable�I��");
    }
    */
    protected override void Start()
    {
        Debug.Log("Start�J�n");
        this.updateItem();
        Debug.Log("Start�I��");
    }

    public void onUpdateItem(int index)
    {
        Debug.Log("onupdateItem�J�n");
        //Debug.Log(realTimeSchroll.chatRoomList.Count);
        Debug.Log(index);
        foreach (ChatListResult clr in realTimeSchroll.chatRoomList)
        {
            Debug.Log(clr.id + clr.chat_room_name);
        }
        Debug.Log(realTimeSchroll.chatRoomList.Count);

        if (realTimeSchroll.chatRoomList.Count > index)
        {
            this.dataIndex = index;
            this.updateItem();
        }
        Debug.Log("onupdateItem�I��");
    }


    private void updateItem()
    {
        Debug.Log("updateItem�J�n");
        if (this.dataIndex == -1) return;

        var data = realTimeSchroll.chatRoomList[this.dataIndex];

        this.background.color = this.colors[Mathf.Abs(this.dataIndex) % this.colors.Length];

        this.title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);

        /*
        if (chatRoomList.on)
        {
            this.title.text = data.name + "(" + data.value + ")";
        }
        else
        {
            this.title.text = data.name + "(" + this.dataIndex.ToString("000") + ")";
        }
        */
        Debug.Log("updateItem�I��");
    }

}
