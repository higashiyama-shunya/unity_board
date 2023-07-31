using Mosframe;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ScrollView1 : MonoBehaviour
{

    public List<ChatListResult> list = new List<ChatListResult>();

    /*  GetComponentでスクリプトを取得するように変更した為コメントアウト
    [SerializeField]
    DynamicScrollView dynamicScrollView;
    */

    private void Start()
    {
        DynamicScrollView dynamicScrollView;
        dynamicScrollView = gameObject.GetComponent<DynamicScrollView>();
        for (int i = 1; i < 6; i++)
        {
            list.Add(new ChatListResult() { id = i, chat_room_name = "room" + i, owner_id = i });
        }
        dynamicScrollView.totalItemCount = list.Count;
    }
}
