using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class JSONEdit : MonoBehaviour
{

    //�ڑ�����URL
    private string URL = "http://192.168.56.56/api/chat_pages/";

    public InputField inputField;
    public InputField inputField2;
    public InputField inputField3;
    //public Text text;
    //public Text text2;
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        id = JSONUpdate.GetId().ToString();
        inputField = inputField.GetComponent<InputField>();
        //text = text.GetComponent<Text>();
        inputField2 = inputField2.GetComponent<InputField>();
        //text2 = text2.GetComponent<Text>();
        inputField3 = inputField3.GetComponent<InputField>();
    }

    public void InputText()
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
        //text.text = inputField.text;
        //text2.text = inputField2.text;

    }

    public void OnClick()
    {
        //�R���[�`�����Ăяo��
        URL += id;
        //InputText();
        StartCoroutine("OnSend", URL);
        SceneManager.LoadScene("SampleScene");
    }


    IEnumerator OnSend(string url)
    {
        //POST������
        WWWForm form = new WWWForm();
        form.AddField("chat_name", inputField.text);
        form.AddField("user_id", inputField3.text);
        form.AddField("chat", inputField2.text);

        //URL��POST�ŗp��
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        //UnityWebRequest�Ƀo�b�t�@���Z�b�g
        webRequest.method = "Put";
        yield return webRequest.SendWebRequest();
        //URL�ɐڑ����Č��ʂ��߂��Ă���܂őҋ@

        //�G���[���o�Ă��Ȃ����`�F�b�N
        if (webRequest.isNetworkError)
        {
            //�ʐM���s
            Debug.Log(webRequest.error);
        }
        else
        {
            //�ʐM����
            Debug.Log(webRequest.downloadHandler.text);
        }
    }
}
