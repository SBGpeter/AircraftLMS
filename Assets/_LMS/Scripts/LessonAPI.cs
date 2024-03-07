using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonAPI : MonoBehaviour
{
    static LessonAPI instance;
    private void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(this);
        //    return;
        //}

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static LessonAPI Instance
    {
        get { return instance; }
    }

    public void OnUpdate()
    {
        Debug.Log("Ÿ�ӽ�����");
        X2R.HTTP.HTTPManager.Instance.OnUpdateLearning();
    }

    public void OnButtonLessonA01() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(19, 52, 54); }
    //J47
    public void OnButtonLessonA02() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(19, 84, 86); }
    //Bypass
}