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

    //ÉRÉãÅ[É`Éì
    void LoadScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

}
