using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTimeStamp : MonoBehaviour
{
    public void Teste()
    {
        LessonAPI.Instance.OnUpdate();
    }
}