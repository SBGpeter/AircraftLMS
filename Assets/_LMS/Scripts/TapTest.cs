using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapTest : MonoBehaviour
{
    public TMP_InputField A;
    public TMP_InputField B;

    public void Update()
    {
        if(A.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                B.Select();
            }
        }
    }
}