using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isEnemy;
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir, bool isEnemy)
    {
        this.isEnemy = isEnemy;
        this.damage = damage;
        this.per = per;

        if (per > -100)
        {
            if (isEnemy)
                rigid.velocity = dir * 2;
            else
                rigid.velocity = dir * 10;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (per == -100 || (isEnemy == false && !collision.CompareTag("Enemy")) || (isEnemy == true && !collision.CompareTag("Player")))
            return;

        per--;

        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.tag = "Untagged";
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.tag = "Untagged";
        gameObject.SetActive(false);
    }
}