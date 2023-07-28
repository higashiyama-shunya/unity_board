using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGet : MonoBehaviour
{
    void Start()
    {
        ScrollView1 scrollView1;
        GameObject chatobj = GameObject.Find("obj");
        Debug.Log(chatobj);
        scrollView1 = chatobj.GetComponent<ScrollView1>();
        Debug.Log(scrollView1);
        Debug.Log(scrollView1.list[0].chat_room_name);
    }
}
