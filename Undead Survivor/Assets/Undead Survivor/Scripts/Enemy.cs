using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float dmg;
    public float rangeDelay;

    public LayerMask targertLayer;

    float timer;
    
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();

        wait = new WaitForFixedUpdate();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive || rangeDelay == -1)
            return;

        timer += Time.deltaTime;
        if (timer > rangeDelay)
        {
            timer = 0f;
            StartCoroutine(Fire());
            Fire();
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        //원거리 적의 경우 범위 안에 플레이어가 있으면 플레이어 반대 방향으로 이동
        if (rangeDelay > 0)
        {
            RaycastHit2D playerRay = Physics2D.CircleCast(transform.position, 3, Vector2.zero, 0, targertLayer);
            if (playerRay)
                nextVec = -nextVec;
        }

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        spriter.color = data.color;
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
        dmg = data.dmg;
        rangeDelay = data.rangeDelay;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            StartCoroutine(KnockBack());
            if (collision.GetComponent<Bullet>().per == -100)
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Melee);
            else
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            gameObject.tag = "Untagged";
            anim.SetBool("Dead", true);

            if (GameManager.Instance.isLive)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
                GameManager.Instance.kill++;
                GameManager.Instance.totalKill++;
                GameManager.Instance.GetExp();
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Fire()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        transform.GetChild(1).gameObject.SetActive(false);
        Transform bullet = GameManager.Instance.pool.Get(3).transform;
        bullet.position = transform.position + new Vector3(0, 0.7f, 0);
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(dmg, 0, dir, true);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }
}