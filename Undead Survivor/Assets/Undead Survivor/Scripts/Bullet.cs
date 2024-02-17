using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isEnemy;
    public float damage;
    public int per;

    bool isInArea;
    float disabledTimer;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isInArea)
        {
            disabledTimer += Time.deltaTime;
            if (disabledTimer > 1)
            {
                gameObject.tag = "Untagged";
                gameObject.SetActive(false);
                disabledTimer = 0;
            }
        }
    }

    public void Init(bool isEnemy, float damage, int per, Vector3 dir, float speed)
    {
        this.isEnemy = isEnemy;
        this.damage = damage;
        this.per = per;

        if (per > -100)
        {
            rigid.velocity = dir * speed;
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

        isInArea = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        isInArea = true;
    }
}