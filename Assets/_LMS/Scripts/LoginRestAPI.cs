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

    //�α��� â
    public GameObject login;
    //�α��� ���� �����޽���
    public GameObject loginErrorMsg;
    //�����ƴ� �����޽���
    public GameObject courseErrorMsg;


    //�α��� �����ϰ� �Ѿ �� �̸�
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
                    Debug.Log("�α��� ����, ���̵�� ��й�ȣ�� ���� �ʽ��ϴ�.");
                    loginErrorMsg.SetActive(true);
                }
            }
            else if (api == API.getRecentlyEnrolledCourseId)
            {
                if (succeed == true)
                {
                    Debug.Log("�α��� ����");
                    SceneManager.LoadScene(LoadSceneName);
                }
                else
                {
                    Debug.Log("�α��� ����, �� ���Ǹ� �������� �ʽ��ϴ�.");
                    courseErrorMsg.SetActive(true);
                }
            }
        }
    }
}
