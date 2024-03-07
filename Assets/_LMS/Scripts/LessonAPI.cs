using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonAPI : MonoBehaviour
{
    public int Content_id;

    static LessonAPI instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static LessonAPI Instance
    {
        get { return instance; }
    }

    public void OnUpdate()
    {
        Debug.Log("≈∏¿”Ω∫≈∆«¡");
        X2R.HTTP.HTTPManager.Instance.OnUpdateLearning();
    }

    public void OnButtonLesson(int x)
    {
        X2R.HTTP.HTTPManager.Instance.OnStartLearning(Content_id, x, x + 2);
    }
}