using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLesson : MonoBehaviour
{
    public void SelectContent(int code)
    {
        LessonAPI.Instance.OnButtonLesson(code);
    }

    //public void J47()
    //{
    //    LessonAPI.Instance.OnButtonLessonA01(Content_id, 52);
    //}

    //public void Bypass()
    //{
    //    LessonAPI.Instance.OnButtonLessonA02(Content_id, 84);
    //}
}