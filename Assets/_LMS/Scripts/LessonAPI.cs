using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonAPI : MonoBehaviour
{
    public void OnButtonLessonA01() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(19, 52, 54); }
    //J47
    public void OnButtonLessonA02() { X2R.HTTP.HTTPManager.Instance.OnStartLearning(19, 84, 86); }
    //Bypass
}