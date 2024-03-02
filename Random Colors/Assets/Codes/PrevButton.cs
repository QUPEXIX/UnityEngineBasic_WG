using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrevButton : MonoBehaviour
{
    public ColorChange change;

    public void ApplyColor()
    {
        change.targetColor = GetComponent<Image>().color;
        GetComponent<Image>().color = change.gameObject.GetComponent<Image>().color;
    }
}