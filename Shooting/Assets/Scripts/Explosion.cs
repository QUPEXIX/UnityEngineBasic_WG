using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("Disable", 0.5f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartExplosion(char target)
    {
        anim.SetTrigger("OnExplosion");

        switch (target)
        {
            case 'L':
                transform.localScale = Vector3.one * 2f;
                break;
            case 'M':
            case 'P':
                transform.localScale = Vector3.one * 1f;
                break;
            case 'S':
                transform.localScale = Vector3.one * 0.5f;
                break;
            case 'B':
                transform.localScale = Vector3.one * 3f;
                break;
        }
    }
}
