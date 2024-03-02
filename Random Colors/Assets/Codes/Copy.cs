using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Copy : MonoBehaviour
{
    public void CopyCode()
    {
        GUIUtility.systemCopyBuffer = GetComponent<TMP_Text>().text;
        GameManager.instance.MessageBox(true);
    }
}