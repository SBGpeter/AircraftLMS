using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    Rest_api rest_api = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public TMP_InputField id;
    public GameObject loginFail;

    [Space(3)]
    [Header("[ SceneHandler ]")]
    public string sceneName;

    public void SceneMoveHandler()
    {
        SceneManager.LoadScene(sceneName);
        rest_api.SceneUpdateOnPut(sceneName);
    }

    public void AppExit()
    {
        Application.Quit();
    }

    /////////////////////////////////////////Login/////////////////////////////////////////
    public void Login()
    {
        rest_api.idText = id.text;
        rest_api.IdFromDatabase();
    }

    public void LoginFailure()
    {
        loginFail.SetActive(true);
    }
}