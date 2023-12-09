using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health;
    private bool isTouchTop;
    private bool isTouchBottom;
    private bool isTouchRight;
    private bool isTouchLeft;

    public float power;
    public float maxShotDelay;
    private float curShotDelay;
    public float maxHitDelay;
    private float curHitDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameManager manager;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
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
        if (!Input.GetButton("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet1 = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 2:
                GameObject bullet2R = Instantiate(bulletObjA, transform.position + Vector3.right*0.2f, transform.rotation);
                GameObject bullet2L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, transform.rotation);
                Rigidbody2D rigid2R = bullet2R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid2L = bullet2L.GetComponent<Rigidbody2D>();
                rigid2R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid2L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 3:
                GameObject bullet3R = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bullet3C = Instantiate(bulletObjA, transform.position + Vector3.up * 0.2f, transform.rotation);
                GameObject bullet3L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigid3R = bullet3R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3C = bullet3C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid3L = bullet3L.GetComponent<Rigidbody2D>();
                rigid3R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid3L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 4:
                GameObject bullet4 = Instantiate(bulletObjB, transform.position, transform.rotation);
                Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();
                rigid4.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 5:
                GameObject bullet5R = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bullet5C = Instantiate(bulletObjB, transform.position + Vector3.up * 0.2f, transform.rotation);
                GameObject bullet5L = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigid5R = bullet5R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5C = bullet5C.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid5L = bullet5L.GetComponent<Rigidbody2D>();
                rigid5R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid5L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 6:
                GameObject bullet6R = Instantiate(bulletObjB, transform.position + Vector3.right * 0.2f, transform.rotation);
                GameObject bullet6L = Instantiate(bulletObjB, transform.position + Vector3.left * 0.2f, transform.rotation);
                Rigidbody2D rigid6R = bullet6R.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid6L = bullet6L.GetComponent<Rigidbody2D>();
                rigid6R.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid6L.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 7:
                GameObject bullet7R = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bullet7C = Instantiate(bulletObjB, transform.position + Vector3.up * 0.2f, transform.rotation);
                GameObject bullet7L = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
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

        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (curHitDelay < maxHitDelay)
                return;
            health -= collision.gameObject.GetComponent<Bullet>().dmg;
            curHitDelay = 0;
            if (health <= 0)
            {
                manager.RespawnPlayer();
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (curHitDelay < maxHitDelay)
                return;
            health -= 1;
            curHitDelay = 0;
            if (health <= 0)
            {
                manager.RespawnPlayer();
                gameObject.SetActive(false);
            }

        }
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