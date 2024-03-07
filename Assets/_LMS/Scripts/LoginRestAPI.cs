using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginRestAPI : MonoBehaviour
{
    public string id;
    public string pw;
    public string courseCodeCode;
    public string courseContentCode;

    public TMP_InputField input_ID, input_PW;

    public GameObject login;
    public GameObject ErrorMsg;

    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void LoginAPI()
    {
        id = input_ID.text;
        pw = input_PW.text;

        X2R.HTTP.HTTPManager.Instance.Init(id, pw, courseCodeCode, courseContentCode, OnRecivePacket);

        X2R.HTTP.HTTPManager.Instance.OnLogin();

    }

    public void OnRecivePacket(bool succeed, string error_code, string message)
    {
        Debug.Log($"{succeed}/{error_code}/{message}");

        if(scene.name == "Scene_Login")
        {
            if (succeed)
            {
                SceneManager.LoadScene("Scene_00_MainMenu");
            }
            else
            {
                login.SetActive(false);
                ErrorMsg.SetActive(true);
            }
        }
    }
}
