using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ScrollItem2 : MonoBehaviour, IDynamicScrollViewItem
{

    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

    [SerializeField]
    public Text title;
    [SerializeField]
    public Image background;

    //それぞれのアイテムを識別するためのindex番号
    private int itemIndex;

    //チャットルームに移動する際に必要な番号
    public int room_id;
    public string room_title;

    //ダイアログポップ用のオブジェクト
    [SerializeField] private AnimatedDialog animatedDialog;

    [SerializeField] private Text RoomText;

    //ダイアログが開く用のメソッド
    public void OnClick()
    {
        ScrollView2 scrollView2;
        GameObject obj = transform.parent.parent.parent.gameObject;
        scrollView2 = obj.GetComponent<ScrollView2>();
        List<ChatListResult> chatListResultList = scrollView2.list;
        Debug.Log(chatListResultList[itemIndex].chat_room_name + "が押されました。");
        RoomText.text = chatListResultList[itemIndex].chat_room_name + "\n オーナーid:" + chatListResultList[itemIndex].owner_id;
        animatedDialog.Open();
    }

    //ルームの名前を表示するためのメソッド、スクロールされるときに起動する。
    public void onUpdateItem(int index)
    {
        itemIndex = index;
        Debug.Log(itemIndex);
        Debug.Log("onUpdateItem開始" + index);
        ScrollView2 scrollView2;
        //探し方を変える
        //親のオブジェクトを探すように
        //GameObject obj = GameObject.Find("Vertical Scroll View"); //名前で探すと複数あった際に良くない
        GameObject obj = transform.parent.parent.parent.gameObject;
        Debug.Log(obj);
        scrollView2 = obj.GetComponent<ScrollView2>();
        Debug.Log(scrollView2);
        Debug.Log(scrollView2.list[index].id + scrollView2.list[index].chat_room_name);
        List<ChatListResult> chatListResultList = scrollView2.list;
        if (chatListResultList.Count > index)
        {
            var data = chatListResultList[index];

            background.color = this.colors[Mathf.Abs(index) % colors.Length];

            title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);
        }
        Debug.Log("onUpdateItem終了" + index);
    }
}
