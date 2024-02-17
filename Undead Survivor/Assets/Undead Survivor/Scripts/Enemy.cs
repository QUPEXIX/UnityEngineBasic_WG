using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBoss;

    public float speed;
    public float health;
    public float maxHealth;
    public float basicDmg;
    public float dmg;

    public LayerMask targertLayer;
    
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;

    public enum Patterns { Normal, Range, Rush }
    [Header("패턴")]
    public Patterns[] patterns;

    public float timer;

    public float rangeDmg;
    public float rangeDelay;
    public float rangeCast;
    public float bulletSpeed;
    public float bulletSize;
    public float bulletDelay;
    public float bulletLocation;

    public float rushDmg;
    public float rushDelay;
    public float rushCastOut; //적 기준 바깥쪽 원
    public float rushCastIn; //적 기준 안쪽 원
    public float rushSpeed;

    bool isRushWating;
    bool isRushing;
    Vector2 rushDir;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();

        wait = new WaitForFixedUpdate();
        timer = 0;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (HavePattern(Patterns.Range) && !isRushing)
        {
            timer += Time.deltaTime;
            if (timer > rangeDelay)
            {
                timer = 0f;
                StartCoroutine(Fire());
            }
        }
        if (HavePattern(Patterns.Rush))
        {
            if (!isRushing && !isRushWating)
            {
                RaycastHit2D playerRay = Physics2D.CircleCast(transform.position, rushCastOut, Vector2.zero, 0, targertLayer);

                if (playerRay)
                {
                    playerRay = Physics2D.CircleCast(transform.position, rushCastIn, Vector2.zero, 0, targertLayer);
                    if (!playerRay)
                        StartCoroutine(RushStart());
                }
            }
            if (isRushing) //돌진 멈추기
            {
                Vector2 curTarDir = (target.transform.position - transform.position).normalized;
                if (curTarDir.x * rushDir.x < 0 || curTarDir.y * rushDir.y < 0)
                {
                    RaycastHit2D playerRay = Physics2D.CircleCast(transform.position, rushCastOut, Vector2.zero, rushCastOut + 0.5f, targertLayer);
                    if (!playerRay)
                        StartCoroutine(RushStop());
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
            
        if (HavePattern(Patterns.Normal) && !isRushWating)
        {
            Vector2 dirVec;
            Vector2 nextVec;

            //돌진 중인지에 따라 이동 로직 변경
            if (isRushing)
            {
                nextVec = rushDir * rushSpeed * Time.fixedDeltaTime;
            }
            else
            {
                dirVec = target.position - rigid.position;
                nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

                //원거리 적의 경우 범위 안에 플레이어가 있으면 플레이어 반대 방향으로 이동
                if (HavePattern(Patterns.Range))
                {
                    RaycastHit2D playerRay = Physics2D.CircleCast(transform.position, rangeCast, Vector2.zero, 0, targertLayer);
                    if (playerRay)
                        nextVec = -nextVec;
                }
            }

            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive)
            return;

        if (!isRushing)
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
        transform.localScale = new Vector3(data.size, data.size, data.size);
        spriter.color = data.color;
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
        basicDmg = data.dmg;
        dmg = data.dmg;

        patterns = data.patterns;
        rangeDmg = data.patternVars[0];
        rangeDelay = data.patternVars[1];
        rangeCast = data.patternVars[2];
        bulletSpeed = data.patternVars[3];
        bulletSize = data.patternVars[4];
        bulletDelay = data.patternVars[5];
        bulletLocation = data.patternVars[6];

        rushDmg = data.patternVars[7];
        rushDelay = data.patternVars[8];
        rushCastOut = data.patternVars[9];
        rushCastIn = data.patternVars[10];
        rushSpeed = data.patternVars[11];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            if (!isRushing && !isBoss)
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
            StopCoroutine(RushStart());

            if (GameManager.Instance.isLive)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
                GameManager.Instance.kill++;
                GameManager.Instance.totalKill++;
                GameManager.Instance.GetExp();
            }

            if (isBoss)
            {
                GameManager.Instance.GameVictory();
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

    IEnumerator Fire()
    {
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        if (bulletDelay > 0)
            transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).transform.localScale = new Vector3(bulletSize / transform.localScale.x, bulletSize / transform.localScale.y, bulletSize / transform.localScale.z);

        yield return new WaitForSeconds(bulletDelay);

        transform.GetChild(1).gameObject.SetActive(false);
        Transform bullet = GameManager.Instance.pool.Get(3).transform;
        bullet.position = transform.position + new Vector3(0, bulletLocation, 0);
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        bullet.GetComponent<Bullet>().Init(true, rangeDmg, 0, dir, bulletSpeed);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

    IEnumerator RushStart()
    {
        isRushWating = true;
        StartCoroutine(RushDelayColor());

        yield return new WaitForSeconds(rushDelay);

        isRushWating = false;
        isRushing = true;
        dmg = rushDmg;
        rushDir = (target.transform.position - transform.position).normalized;
    }

    IEnumerator RushStop()
    {
        yield return new WaitForSeconds(0.1f);

        dmg = basicDmg;
        isRushing = false;
    }

    IEnumerator RushDelayColor()
    {
        while (!isRushing)
        {
            spriter.color = new Color(spriter.color.r, spriter.color.g, spriter.color.b, 0.3f);

            yield return new WaitForSeconds(0.1f);

            spriter.color = new Color(spriter.color.r, spriter.color.g, spriter.color.b, 1);

            yield return new WaitForSeconds(0.3f);
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    bool HavePattern(Patterns patt)
    {
        if (Array.IndexOf(patterns, patt) == -1)
            return false;
        else
            return true;
    }
}