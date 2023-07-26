using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour, IDynamicScrollViewItem
{
    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
        };

    [SerializeField]
    public Text title;
    [SerializeField]
    public Image background;
    [SerializeField]
    public ScrollView scrollView;

    public void onUpdateItem(int index)
    {
        List<ChatListResult> chatListResultList = ScrollView.getChatListResultList();
        if (chatListResultList.Count > index)
        {
            var data = chatListResultList[index];

            background.color = this.colors[Mathf.Abs(index) % colors.Length];

            title.text = string.Format("{0}:{1}", data.id, data.chat_room_name);
        }
    }
}
