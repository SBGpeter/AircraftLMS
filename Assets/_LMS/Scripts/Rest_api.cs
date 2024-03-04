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
        {// Get이 없을 때 실행

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

        User user = new User(); // 새로운 계정 및 기존 계정 수정하기 위해 필요 즉, Put과 Post 둘다 가능
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
            //    Debug.Log("로그인 성공!");
            //    GameManager.Instance.LoginSuccess();

            //}
            //else
            //{
            //    Debug.Log("PW가 불일치합니다.");
            //    GameManager.Instance.PasswordWrong();
            //}
        }).Catch(error =>
        {// 아이디가 존재하지 않을 시 실행 부분
            GameManager.Instance.LoginFailure();
        });
    }
}
