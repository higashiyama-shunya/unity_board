using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SchrollItem : MonoBehaviour, IDynamicScrollViewItem
{
    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

    /*
    public static List<ChatListResult> list;

    [SerializeField]
    public DynamicScrollView scrollView;
    */
    [SerializeField]
    public Text title;
    [SerializeField]
    public Image background;

    //onUpdateItem:�X�N���[������Ƃ��ɋN������
    //���[�����Ȃǂ��X�V���ĕ\������B
    public void onUpdateItem(int index)
    {
        Debug.Log("onUpdateItem�J�n:" + index);
        Debug.Log("listcount:" + SchrollView.i.list.Count + "\n index:" + index);
        if (SchrollView.i.list.Count > index)
        {
            var data = SchrollView.i.list[index];

            background.color = this.colors[Mathf.Abs(index) % colors.Length];

            title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);
        }
        Debug.Log("onUpdateItem�I��:" + index);
    }

}