using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLesson : MonoBehaviour
{
    public void J47()
    {
        LessonAPI.Instance.OnButtonLessonA01();
    }

    public void Bypass()
    {
        LessonAPI.Instance.OnButtonLessonA02();
    }
}