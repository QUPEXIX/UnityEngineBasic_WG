using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public char enemyName;
    public float speed;
    public int health;
    public int score;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    public float maxShotDelay;
    private float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public GameObject itemBoom;
    public GameObject itemCoin;
    public GameObject itemPower;
    public ObjectManager objectManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
    }


    void Update()
    {
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
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            player.GetComponent<Player>().score += score;
            int ran = Random.Range(0, 12);
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
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderEnemy")
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