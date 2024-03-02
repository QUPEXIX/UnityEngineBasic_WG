using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] prevButtons;
    public GameObject messageBox;

    bool canQuit;

    void Awake()
    {
        instance = this;
        Screen.fullScreen = false;
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        canQuit = false;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!canQuit)
                {
                    MessageBox(false);
                    StartCoroutine(QuitTime());
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

    IEnumerator QuitTime()
    {
        yield return null;

        canQuit = true;

        yield return new WaitForSeconds(2);

        canQuit = false;
    }

    public void MessageBox(bool isCopy)
    {
        StopAllCoroutines();
        StartCoroutine(MessageBoxCoroutine(isCopy));
    }

    IEnumerator MessageBoxCoroutine(bool isCopy)
    {
        messageBox.SetActive(true);

        if (isCopy)
            messageBox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ŭ�����忡 ����Ǿ����ϴ�.";
        else
            messageBox.transform.GetChild(0).GetComponent<TMP_Text>().text = "�� �� �� ������ ���� ����˴ϴ�.";

        messageBox.GetComponent<RectTransform>().sizeDelta = new Vector2(isCopy ? 520 : 588, messageBox.GetComponent<RectTransform>().rect.height);

        yield return new WaitForSeconds(isCopy ? 1 : 2);

        messageBox.SetActive(false);
    }
}