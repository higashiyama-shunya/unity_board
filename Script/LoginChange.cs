using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginChange : MonoBehaviour
{
    public void OnClick()
    {
        LoadScene();
    }

    //�R���[�`��
    void LoadScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

}
