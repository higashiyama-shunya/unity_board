using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SchrollView : MonoBehaviour
{
    //作成したデータを保持するstatic変数
    public static SchrollView i;

    //ルームリストのリスト
    public List<ChatListResult> list = new List<ChatListResult>();

    //inspecterからアクセス可能にするため
    [SerializeField]
    DynamicScrollView dynamicScrollView;

    //Awakeで変数Iに自分自身を代入させる。

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    //リストを初期化する
    void Start()
    {
        list = new List<ChatListResult>();
        for (int i = 1; i < 4; i++)
        {
            list.Add(new ChatListResult() { id = i, chat_room_name = "room" + i, owner_id = i });
        }
        //Debug.Log("listcount:" + list.Count + "\ntotalItemCount" + scrollView.totalItemCount);
        dynamicScrollView.totalItemCount = list.Count;
        Debug.Log("listcount:" + list.Count + "\ntotalItemCount" + dynamicScrollView.totalItemCount);
    }
}
