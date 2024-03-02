using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    Image image;
    public TMP_Text titleText;
    public TMP_Text rgbText;
    public TMP_Text hsvText;
    public TMP_Text hexText;
    public TMP_Text noticeText;

    public Color targetColor;

    void Awake()
    {
        image = GetComponent<Image>();
        ChangeColor(true);
    }

    public void ChangeColor(bool isRandom)
    {
        byte r;
        byte g;
        byte b;

        if (isRandom)
        {
            r = (byte)Random.Range(0, 256);
            g = (byte)Random.Range(0, 256);
            b = (byte)Random.Range(0, 256);
        }
        else
        {
            r = (byte)Mathf.RoundToInt(targetColor.r * 255);
            g = (byte)Mathf.RoundToInt(targetColor.g * 255);
            b = (byte)Mathf.RoundToInt(targetColor.b * 255);
        }

        image.color = new Color32(r, g, b, 255);
        Color.RGBToHSV(image.color, out float floatH, out float floatS, out float floatV);
        int h = Mathf.RoundToInt(floatH * 360);
        int s = Mathf.RoundToInt(floatS * 100);
        int v = Mathf.RoundToInt(floatV * 100);

        rgbText.text = "<color=red>R</color> " + r + "\n<color=green>G</color> " + g + "\n<color=blue>B</color> " + b;
        hsvText.text = h + " H\n" + s + " S\n" + v + " V";
        hexText.text = image.color.ToHexString().Remove(6);

        if (image.color.r + image.color.g + image.color.b < 1.5f)
        {
            titleText.color = Color.white;
            rgbText.color = Color.white;
            hsvText.color = Color.white;
            hexText.color = Color.white;
            noticeText.color = Color.white;
        }
        else
        {
            titleText.color = Color.black;
            rgbText.color = Color.black;
            hsvText.color = Color.black;
            hexText.color = Color.black;
            noticeText.color = Color.black;
        }
    }

    public void PrevColor()
    {
        bool isFull = true;

        foreach (GameObject obj in GameManager.instance.prevButtons)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                obj.GetComponent<Image>().color = image.color;
                isFull = false;
                break;
            }
        }

        if (isFull)
        {
            for (int i = 0; i < GameManager.instance.prevButtons.Length - 1; i++)
            {
                GameManager.instance.prevButtons[i].GetComponent<Image>().color = GameManager.instance.prevButtons[i + 1].GetComponent<Image>().color;
            }
            GameManager.instance.prevButtons[GameManager.instance.prevButtons.Length - 1].GetComponent<Image>().color = image.color;
        }
    }
}