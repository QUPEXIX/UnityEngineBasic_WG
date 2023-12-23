using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    private bool isTouchTop;
    private bool isTouchBottom;
    private bool isTouchRight;
    private bool isTouchLeft;

    public int life;
    public int maxLife;
    public int score;
    
    public int power;
    public bool isHaveBoom;
    public int maxPower;
    public float maxShotDelay;
    private float curShotDelay;
    public float maxHitDelay;
    private float curHitDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    SpriteRenderer spriteRenderer;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public GameObject[] followers;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        life = maxLife;
    }

    void OnDisable()
    {
        Task.Delay(500);
        if (power >= 4)
            followers[0].transform.localPosition = new Vector3(0.73f, 0.53f, 0);
        if (power >= 6)
            followers[1].transform.localPosition = new Vector3(-0.73f, 0.53f, 0);
        if (power >= 8)
            followers[2].transform.localPosition = new Vector3(0, 0.86f, 0);
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
        Boom();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            anim.SetInteger("Input", (int)h);
    }

    void Fire()
    {
        if (!Input.GetButton("Jump"))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet1 = objectManager.MakeObj("BulletPlayerA");
                bullet1.transform.position = transform.position;
                Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 2:
                GameObject bullet2R = objectManager.MakeObj("BulletPlayerA");
                bullet2R.transform.position = transform.position + Vector3.right * 0.2f;
                GameObject bullet2L = objectManager.MakeObj("BulletPlayerA");
                bullet2L.transform.position = transform.position + Vector3.left * 0.2f;
                Rigidbody2D rigid2R = bullet2R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid2L = bullet2L.GetComponent<Rigidbody2D>();
                rigid2R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid2L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 3:
                GameObject bullet3R = objectManager.MakeObj("BulletPlayerA");
                bullet3R.transform.position = transform.position + Vector3.right * 0.3f;
                GameObject bullet3C = objectManager.MakeObj("BulletPlayerA");
                bullet3C.transform.position = transform.position + Vector3.up * 0.2f;
                GameObject bullet3L = objectManager.MakeObj("BulletPlayerA");
                bullet3L.transform.position = transform.position + Vector3.left * 0.3f;
                Rigidbody2D rigid3R = bullet3R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3C = bullet3C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3L = bullet3L.GetComponent<Rigidbody2D>();
                rigid3R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 4:
                GameObject bullet4 = objectManager.MakeObj("BulletPlayerB");
                bullet4.transform.position = transform.position;
                Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();
                rigid4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 5:
                GameObject bullet5R = objectManager.MakeObj("BulletPlayerA");
                bullet5R.transform.position = transform.position + Vector3.right * 0.3f;
                GameObject bullet5C = objectManager.MakeObj("BulletPlayerB");
                bullet5C.transform.position = transform.position + Vector3.up * 0.2f;
                GameObject bullet5L = objectManager.MakeObj("BulletPlayerA");
                bullet5L.transform.position = transform.position + Vector3.left * 0.3f;
                Rigidbody2D rigid5R = bullet5R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5C = bullet5C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5L = bullet5L.GetComponent<Rigidbody2D>();
                rigid5R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 6:
                GameObject bullet6R = objectManager.MakeObj("BulletPlayerB");
                bullet6R.transform.position = transform.position + Vector3.right * 0.2f;
                GameObject bullet6L = objectManager.MakeObj("BulletPlayerB");
                bullet6L.transform.position = transform.position + Vector3.left * 0.2f;
                Rigidbody2D rigid6R = bullet6R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6L = bullet6L.GetComponent<Rigidbody2D>();
                rigid6R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            default:
                GameObject bullet7R = objectManager.MakeObj("BulletPlayerB");
                bullet7R.transform.position = transform.position + Vector3.right * 0.3f;
                GameObject bullet7C = objectManager.MakeObj("BulletPlayerB");
                bullet7C.transform.position = transform.position + Vector3.up * 0.2f;
                GameObject bullet7L = objectManager.MakeObj("BulletPlayerB");
                bullet7L.transform.position = transform.position + Vector3.left * 0.3f;
                Rigidbody2D rigid7R = bullet7R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid7C = bullet7C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid7L = bullet7L.GetComponent<Rigidbody2D>();
                rigid7R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid7C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid7L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
        curHitDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "TopBorder":
                    isTouchTop = true;
                    break;
                case "BottomBorder":
                    isTouchBottom = true;
                    break;
                case "RightBorder":
                    isTouchRight = true;
                    break;
                case "LeftBorder":
                    isTouchLeft = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (curHitDelay < maxHitDelay)
                return;
            health -= collision.gameObject.GetComponent<Bullet>().dmg;
            collision.gameObject.SetActive(false);
            curHitDelay = 0;
            spriteRenderer.color = new Color(0, 0, 1, 0.5f);
            Invoke("HitColor", maxHitDelay);
            if (health <= 0)
            {
                Die();
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (curHitDelay < maxHitDelay)
                return;
            health -= 1;
            curHitDelay = 0;
            spriteRenderer.color = new Color(0, 0, 1, 0.5f);
            Invoke("HitColor", maxHitDelay);
            if (health <= 0)
            {
                Die();
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Boom":
                    if (isHaveBoom == true)
                        score += 750;
                    else
                    {
                        isHaveBoom = true;
                        gameManager.UpdateBoomIcon();
                    }
                    break;
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power >= maxPower)
                    {
                        score += 500;
                    }
                    else
                    {
                        power++;
                        AddFollower();
                        switch (power)
                        {
                            case 1:
                                gameManager.SpawnDelay(1f, 4f);
                                break;
                            case 2:
                                gameManager.SpawnDelay(1f, 3.5f);
                                break;
                            case 3:
                                gameManager.SpawnDelay(1f, 3f);
                                break;
                            case 4:
                                gameManager.SpawnDelay(1f, 2.5f);
                                break;
                            case 5:
                                gameManager.SpawnDelay(1f, 2f);
                                break;
                            case 6:
                                gameManager.SpawnDelay(1f, 1.5f);
                                break;
                            case 7:
                                gameManager.SpawnDelay(1f, 1f);
                                break;
                            case 8:
                                break;
                        }
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        if (power == 4)
            followers[0].SetActive(true);
        if (power == 6)
            followers[1].SetActive(true);
        if (power == 8)
            followers[2].SetActive(true);
    }

    void Boom()
    {
        if (!Input.GetButton("Fire1") || isHaveBoom == false)
            return;

        isHaveBoom = false;
        gameManager.UpdateBoomIcon();
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 3f);

        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        for (int i = 0; i < enemiesL.Length; i++)
        {
            if (enemiesL[i].activeSelf)
            {
                Enemy enemyLogic = enemiesL[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int i = 0; i < enemiesM.Length; i++)
        {
            if (enemiesM[i].activeSelf)
            {
                Enemy enemyLogic = enemiesM[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int i = 0; i < enemiesS.Length; i++)
        {
            if (enemiesS[i].activeSelf)
            {
                Enemy enemyLogic = enemiesS[i].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }

        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
        for (int i = 0; i < bulletsA.Length; i++)
        {
            if (bulletsA[i].activeSelf)
            {
                bulletsA[i].SetActive(false);
            }
        }
        for (int i = 0; i < bulletsB.Length; i++)
        {
            if (bulletsB[i].activeSelf)
            {
                bulletsB[i].SetActive(false);
            }
        }
    }

    void Die()
    {
        life--;
        isHaveBoom = false;
        gameManager.UpdateLifeIcon(life, maxLife);
        gameManager.UpdateBoomIcon();
        if (life == 0)
        {
            gameManager.GameOver();
        }
        else
        {
            gameManager.RespawnPlayer();
        }
        gameObject.SetActive(false);
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
    }

    public void HitColor()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "TopBorder":
                    isTouchTop = false;
                    break;
                case "BottomBorder":
                    isTouchBottom = false;
                    break;
                case "RightBorder":
                    isTouchRight = false;
                    break;
                case "LeftBorder":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}