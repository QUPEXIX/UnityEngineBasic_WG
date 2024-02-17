using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCons;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); //true ���ڰ��� ���� ��Ȱ��ȭ ������ ������Ʈ������ ������Ʈ�� ���� �� ����
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCons[GameManager.Instance.playerId];
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //���� �Ѿ˿� �¾��� ��
    {
        if (collision.CompareTag("Enemy Bullet") == false || !GameManager.Instance.isLive)
            return;

        GameManager.Instance.health -= collision.GetComponent<Bullet>().damage;

        if (GameManager.Instance.health < 0)
            Die();
    }

    void OnCollisionStay2D(Collision2D collision) //������ �¾��� ��
    {
        if (collision.gameObject.CompareTag("Enemy") == false || !GameManager.Instance.isLive)
            return;

        GameManager.Instance.health -= Time.deltaTime * 10 * collision.gameObject.GetComponent<Enemy>().dmg;

        if (GameManager.Instance.health < 0)
            Die();
    }

    void Die()
    {
        GameManager.Instance.hud.SetActive(false);

        for (int i = 1; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        GameManager.Instance.isLive = false;
        anim.SetTrigger("Dead");
        GameManager.Instance.GameOver();
    }
}