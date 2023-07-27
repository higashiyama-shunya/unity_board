using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem1 : MonoBehaviour, IDynamicScrollViewItem
{

    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

    [SerializeField]
    public Text title;
    [SerializeField]
    public Image background;
    /*
    [SerializeField]
    public ScrollView scrollView;
    */

    public void onUpdateItem(int index)
    {
        Debug.Log("onUpdateItem�J�n" + index);
        ScrollView1 scrollView1;
        //�T������ς���
        //�e�̃I�u�W�F�N�g��T���悤��
        //GameObject obj = GameObject.Find("Vertical Scroll View"); //���O�ŒT���ƕ����������ۂɗǂ��Ȃ�
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
        Debug.Log("onUpdateItem�I��" + index);
    }
}
