using Proyecto26;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Models;
using static CW.Common.CwInputManager;

public class Rest_api : MonoBehaviour
{
    User user = new();

    public static bool playerLogin = false;
    public static string playerId;
    //public static string playerPassword;
    public static string playerScene;

    readonly string link = "https://testapi-81614-default-rtdb.asia-southeast1.firebasedatabase.app/";
    readonly string jsonWord = ".json";
    [HideInInspector]
    public string idText;

    private void RetrieveFromDatabase()
    {
        string a = $"{link}{idText.ToLower()}{jsonWord}";

        RestClient.Get<User>(a).Then(response =>
        {
            user = response;
            UpdateScore();
        }).Catch(error =>
        {// Get�� ���� �� ����

        });
    }

    public void UpdateScore()
    {
        //
    }

    public void SceneUpdateOnPut(string sceneName)
    {
        playerScene = sceneName;
        PostToDatabase();
    }

    //public void OnPut()
    //{
    //    playerId = id.text.ToLower();
    //    //playerPassword = pw.text;
    //    PostToDatabase();
    //}

    private void PostToDatabase()
    {
        string a = $"{link}{playerId.ToLower()}{jsonWord}";

        User user = new User(); // ���ο� ���� �� ���� ���� �����ϱ� ���� �ʿ� ��, Put�� Post �Ѵ� ����
        RestClient.Put(a, user);
    }

    /////////////////////////////////////Login////////////////////////////
    public void IdFromDatabase()
    {
        string a = $"{link}{idText.ToLower()}{jsonWord}";

        RestClient.Get<User>(a).Then(response =>
        {
            user = response;

            playerLogin = true;
            playerId = idText;
            GameManager.Instance.SceneMoveHandler();
            PostToDatabase();

            //if (user.userPassword.Equals(pw.text))
            //{
            //    Debug.Log("�α��� ����!");
            //    GameManager.Instance.LoginSuccess();

            //}
            //else
            //{
            //    Debug.Log("PW�� ����ġ�մϴ�.");
            //    GameManager.Instance.PasswordWrong();
            //}
        }).Catch(error =>
        {// ���̵� �������� ���� �� ���� �κ�
            GameManager.Instance.LoginFailure();
        });
    }
}
