using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

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
        ScrollView1 scrollView1;
        GameObject obj = transform.parent.parent.parent.gameObject;
        scrollView1 = obj.GetComponent<ScrollView1>();
        List<ChatListResult> chatListResultList = scrollView1.list;
        Debug.Log(chatListResultList[itemIndex].chat_room_name + "��������܂����B");
        RoomText.text = chatListResultList[itemIndex].chat_room_name + "\n �I�[�i�[id:" + chatListResultList[itemIndex].owner_id;
        animatedDialog.Open();
    }

    //�ړ��p�̃��\�b�h
    //�l��ێ������܂ܕʂ̃V�[���ɍs����������肭�����Ȃ��B
    public void LoadChatRoom()
    {
        ScrollView1 scrollView1;
        GameObject obj = transform.parent.parent.parent.gameObject;
        scrollView1 = obj.GetComponent<ScrollView1>();
        List<ChatListResult> chatListResultList = scrollView1.list;
        room_id = chatListResultList[itemIndex].id;
        room_title = itemIndex.ToString();
        DontDestroyOnLoad(obj);
        SceneManager.LoadScene("ChatScene");
        //SceneManager.LoadScene("ChatScene", LoadSceneMode.Additive);

    }

    //���[���̖��O��\�����邽�߂̃��\�b�h�A�X�N���[�������Ƃ��ɋN������B
    public void onUpdateItem(int index)
    {
        itemIndex = index;
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
