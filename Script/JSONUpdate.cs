using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class JSONUpdate : MonoBehaviour
{
    protected static int id;

    public InputField inputField;

    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
    }

    public void OnClick()
    {
        id = int.Parse(inputField.text);
        LoadScene();
    }

    //ÉRÉãÅ[É`Éì
    void LoadScene()
    {
        SceneManager.LoadScene("UpdateScene");
    }

    public static int GetId()
    {
        return id;
    }
}
