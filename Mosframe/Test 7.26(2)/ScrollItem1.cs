using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ScrollItem1 : MonoBehaviour, IDynamicScrollViewItem
{

    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

    //それぞれのアイテムを識別するためのindex番号
    private int itemIndex;

    [SerializeField]
    public Text title;
    [SerializeField]
    public Image background;

    //ダイアログポップ用のオブジェクト
    [SerializeField] private AnimatedDialog animatedDialog;

    [SerializeField] private Text RoomText;

    public void OnClick()
    {
        ScrollView1 scrollView1;
        GameObject obj = transform.parent.parent.parent.gameObject;
        scrollView1 = obj.GetComponent<ScrollView1>();
        List<ChatListResult> chatListResultList = scrollView1.list;
        Debug.Log(chatListResultList[itemIndex].chat_room_name + "が押されました。");
        RoomText.text = chatListResultList[itemIndex].chat_room_name;
        animatedDialog.Open();
    }


    public void onUpdateItem(int index)
    {
        itemIndex = index;
        Debug.Log("onUpdateItem開始" + index);
        ScrollView1 scrollView1;
        //探し方を変える
        //親のオブジェクトを探すように
        //GameObject obj = GameObject.Find("Vertical Scroll View"); //名前で探すと複数あった際に良くない
        GameObject obj = transform.parent.parent.parent.gameObject;
        Debug.Log(obj);
        scrollView1 = obj.GetComponent<ScrollView1>();
        Debug.Log(scrollView1.list[index].id + scrollView1.list[index].chat_room_name);
        List<ChatListResult> chatListResultList = scrollView1.list;
        if (chatListResultList.Count > index)
        {
            var data = chatListResultList[index];

            background.color = this.colors[Mathf.Abs(index) % colors.Length];

            title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);
        }
        Debug.Log("onUpdateItem終了" + index);
    }
}
