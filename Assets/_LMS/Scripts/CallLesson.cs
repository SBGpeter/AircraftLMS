using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLesson : MonoBehaviour
{
    public void SelectContent(int code)
    { // 52 - J47 / 84 - Bypass
        LessonAPI.Instance.OnButtonLesson(code);
    }
}