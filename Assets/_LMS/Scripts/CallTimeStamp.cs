using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTimeStamp : MonoBehaviour
{
    public void TimeStamp()
    {
        LessonAPI.Instance.OnUpdate();
    }
}