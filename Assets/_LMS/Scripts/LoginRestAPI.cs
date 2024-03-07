using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static X2R.HTTP.HTTPManager;

public class LoginRestAPI : MonoBehaviour
{
    public string id;
    public string pw;
    public string courseCodeCode;
    public string courseContentCode;

    public TMP_InputField input_ID, input_PW;

    //로그인 창
    public GameObject login;
    //로그인 실패 에러메시지
    public GameObject loginErrorMsg;
    //수강아님 에러메시지
    public GameObject courseErrorMsg;


    //로그인 성공하고 넘어갈 씬 이름
    public string LoadSceneName;

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

    public void OnRecivePacket(API api, bool succeed, string error_code, string message)
    {
        Debug.Log($"{succeed}/{error_code}/{message}");

        if(scene.name == "Scene_Login")
        {
            if (api == API.login)
            {
                if (succeed == false)
                {
                    Debug.Log("로그인 실패, 아이디와 비밀번호가 맞지 않습니다.");
                    loginErrorMsg.SetActive(true);
                }
            }
            else if (api == API.getRecentlyEnrolledCourseId)
            {
                if (succeed == true)
                {
                    Debug.Log("로그인 성공");
                    SceneManager.LoadScene(LoadSceneName);
                }
                else
                {
                    Debug.Log("로그인 실패, 이 강의를 수강하지 않습니다.");
                    courseErrorMsg.SetActive(true);
                }
            }
        }
    }
}
