using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjects;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    private float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image boomImage;
    public GameObject gameOverSet;

    float spawnDelayran;

    void Start()
    {
        spawnDelayran = Random.Range(1f, 4f);
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = spawnDelayran;
            curSpawnDelay = 0;
        }
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, enemyObjects.Length);
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyObjects[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if (ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-0.8f), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * 0.8f, -1);
        }
        else
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
    }

    public void SpawnDelay(float startRange, float endRange)
    {
        spawnDelayran = Random.Range(startRange, endRange);
    }

    public void UpdateLifeIcon(int life, int maxLife)
    {
        for (int index = 0;  index < maxLife - life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
    }

    public void UpdateBoomIcon()
    {
        if (player.GetComponent<Player>().isHaveBoom == true)
            boomImage.color = new Color(1, 1, 1, 1);
        else
            boomImage.color = new Color(1, 1, 1, 0);
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4;
        player.SetActive(true);
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.health = playerLogic.maxHealth;
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}