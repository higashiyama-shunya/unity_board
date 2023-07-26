using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SchrollView : MonoBehaviour
{
    //�쐬�����f�[�^��ێ�����static�ϐ�
    public static SchrollView i;

    //���[�����X�g�̃��X�g
    public List<ChatListResult> list = new List<ChatListResult>();

    //inspecter����A�N�Z�X�\�ɂ��邽��
    [SerializeField]
    DynamicScrollView dynamicScrollView;

    //Awake�ŕϐ�I�Ɏ������g����������B

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    //���X�g������������
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
