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

    //���ꂼ��̃A�C�e�������ʂ��邽�߂�index�ԍ�
    private int itemIndex;

    //�`���b�g���[���Ɉړ�����ۂɕK�v�Ȕԍ�
    public int room_id;
    public string room_title;

    //�_�C�A���O�|�b�v�p�̃I�u�W�F�N�g
    [SerializeField] private AnimatedDialog animatedDialog;

    [SerializeField] private Text RoomText;

    //�_�C�A���O���J���p�̃��\�b�h
    public void OnClick()
    {
        ScrollView2 scrollView2;
        GameObject obj = transform.parent.parent.parent.gameObject;
        scrollView2 = obj.GetComponent<ScrollView2>();
        List<ChatListResult> chatListResultList = scrollView2.list;
        Debug.Log(chatListResultList[itemIndex].chat_room_name + "��������܂����B");
        RoomText.text = chatListResultList[itemIndex].chat_room_name + "\n �I�[�i�[id:" + chatListResultList[itemIndex].owner_id;
        animatedDialog.Open();
    }

    //���[���̖��O��\�����邽�߂̃��\�b�h�A�X�N���[�������Ƃ��ɋN������B
    public void onUpdateItem(int index)
    {
        itemIndex = index;
        Debug.Log(itemIndex);
        Debug.Log("onUpdateItem�J�n" + index);
        ScrollView2 scrollView2;
        //�T������ς���
        //�e�̃I�u�W�F�N�g��T���悤��
        //GameObject obj = GameObject.Find("Vertical Scroll View"); //���O�ŒT���ƕ����������ۂɗǂ��Ȃ�
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
        Debug.Log("onUpdateItem�I��" + index);
    }
}
