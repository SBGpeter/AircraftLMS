using Best.HTTP.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMenu : MonoBehaviour
{
    public string id;
    public string pw;
    public string courseCodeCode;
    public string courseContentCode;

    private void Start()
    {
        X2R.HTTP.HTTPManager.Instance.Init(id, pw, courseCodeCode, courseContentCode, OnRecivePacket);
    }

    public void OnRecivePacket(bool succeed, string error_code, string message)
    {
        Debug.Log($"{succeed}/{error_code}/{message}");
    }
    public void OnButtonLogin()
    {
        X2R.HTTP.HTTPManager.Instance.OnLogin();
    }

    public void OnButtonLessonA01() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8,8,10); }
    public void OnButtonLessonA02() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 10, 12); }
    public void OnButtonLessonA03() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 11, 13); }

    public void OnButtonLessonA04() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 12, 14); }
    public void OnButtonLessonA05() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 13, 15); }
    public void OnButtonLessonA06() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 14, 16); }
    public void OnButtonLessonA07() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(8, 15, 17); }

    public void OnButtonLessonB01() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(12, 16, 18); }
}
