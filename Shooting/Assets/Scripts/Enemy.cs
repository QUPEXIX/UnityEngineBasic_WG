using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public char enemyName;
    public float speed;
    public int health;
    public int score;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public float maxShotDelay;
    private float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public GameObject itemBoom;
    public GameObject itemCoin;
    public GameObject itemPower;

    public ObjectManager objectManager;
    public GameManager gameManager;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == 'B')
            anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case 'L':
                health = 15;
                break;
            case 'M':
                health = 5;
                break;
            case 'S':
                health = 3;
                break;
            case 'B':
                health = 1500;
                Invoke("Stop", 2.3f);
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 1);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        GameObject bulletBR1 = objectManager.MakeObj("BulletBossA");
        bulletBR1.transform.position = transform.position + Vector3.right * 0.65f;
        GameObject bulletBR2 = objectManager.MakeObj("BulletBossA");
        bulletBR2.transform.position = transform.position + Vector3.right * 0.85f;
        GameObject bulletBL1 = objectManager.MakeObj("BulletBossA");
        bulletBL1.transform.position = transform.position + Vector3.left * 0.65f;
        GameObject bulletBL2 = objectManager.MakeObj("BulletBossA");
        bulletBL2.transform.position = transform.position + Vector3.left * 0.85f;
        GameObject bulletC = objectManager.MakeObj("BulletBossA");
        bulletC.transform.position = transform.position;

        Rigidbody2D rigidBR1 = bulletBR1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBR2 = bulletBR2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBL1 = bulletBL1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBL2 = bulletBL2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();

        rigidBR1.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidBR2.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidBL1.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidBL2.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidC.AddForce(Vector2.down * 4, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 0.5f);
        else
            Invoke("Think", 2);
    }
    void FireShot()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bulletS = objectManager.MakeObj("BulletEnemyB");
            bulletS.transform.position = transform.position;

            Rigidbody2D rigidS = bulletS.GetComponent<Rigidbody2D>();

            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-2, 2), Random.Range(0f, 5f));
            dirVec += ranVec;
            rigidS.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 1);
        else
            Invoke("Think", 2);
    }
    void FireArc()
    {
        GameObject bulletA = objectManager.MakeObj("BulletEnemyA");
        bulletA.transform.position = transform.position;
        bulletA.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 8 * curPatternCount/maxPatternCount[patternIndex]), -1);

        rigidA.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.1f);
        else
            Invoke("Think", 2);
    }
    void FireAround()
    {
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount%2 == 0 ? roundNumA : roundNumB;

        for (int i = 0; i < roundNum; i++)
        {
            GameObject bulletA = objectManager.MakeObj("BulletBossB");
            bulletA.transform.position = transform.position;
            bulletA.transform.rotation = Quaternion.identity;

            Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum), Mathf.Sin(Mathf.PI * 2 * i / roundNum));

            rigidA.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bulletA.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.8f);
        else
            Invoke("Think", 2);
    }

    void Update()
    {
        if (enemyName == 'B')
            return;

        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        if (enemyName == 'S')
        {
            GameObject bulletS = objectManager.MakeObj("BulletEnemyA");
            bulletS.transform.position = transform.position;
            Rigidbody2D rigidS = bulletS.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigidS.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
            curShotDelay = 0;
            return;
        }
        else if (enemyName == 'L')
        {
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position;
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigidL.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);
            curShotDelay = 0;
            return;
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;
        health -= dmg;

        if (enemyName == 'B')
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }

        if (health <= 0)
        {
            player.GetComponent<Player>().score += score;
            int ran = enemyName == 'B' ? 0 : Random.Range(0, 12);
            if (ran == 0)
            {
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            else if (ran < 3)
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 6)
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);

            if (enemyName == 'B')
            {
                gameManager.StageEnd();
            }
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderEnemy" && enemyName != 'B')
        {
            player.GetComponent<Player>().score -= score;
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}