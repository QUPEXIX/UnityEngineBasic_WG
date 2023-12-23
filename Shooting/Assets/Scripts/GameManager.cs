using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjects;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    private float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image boomImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;

    float spawnDelayran;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjects = new string[] { "EnemyL", "EnemyM", "EnemyS" };
        ReadSpawnFile();
    }

    void Start()
    {
        spawnDelayran = Random.Range(1f, 4f);
    }

    void ReadSpawnFile()
    {
        //변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //리스폰 파일 읽기
        TextAsset textFile = Resources.Load("Stage 0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;
            //리스폰 데이터 생성
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        //텍스트 파일 닫기
        stringReader.Close();

        //첫 번째 스폰 딜레이 적용
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "L":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "S":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjects[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-0.8f), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * 0.8f, -1);
        }
        else
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

        //리스폰 인덱스 증가
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    //적 랜덤 스폰
    //void Update()
    //{
    //    curSpawnDelay += Time.deltaTime;
    //    if (curSpawnDelay > nextSpawnDelay)
    //    {
    //        RandomSpawnEnemy();
    //        nextSpawnDelay = spawnDelayran;
    //        curSpawnDelay = 0;
    //    }
    //    Player playerLogic = player.GetComponent<Player>();
    //    scoreText.text = string.Format("{0:n0}", playerLogic.score);
    //}

    //void RandomSpawnEnemy()
    //{
    //    int ranEnemy = Random.Range(0, enemyObjects.Length);
    //    int ranPoint = Random.Range(0, spawnPoints.Length);
    //    GameObject enemy = objectManager.MakeObj(enemyObjects[ranEnemy]);
    //    enemy.transform.position = spawnPoints[ranPoint].position;

    //    Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
    //    Enemy enemyLogic = enemy.GetComponent<Enemy>();
    //    enemyLogic.player = player;
    //    enemyLogic.objectManager = objectManager;

    //    if (ranPoint == 5 || ranPoint == 6)
    //    {
    //        enemy.transform.Rotate(Vector3.back * 90);
    //        rigid.velocity = new Vector2(enemyLogic.speed * (-0.8f), -1);
    //    }
    //    else if (ranPoint == 7 || ranPoint == 8)
    //    {
    //        enemy.transform.Rotate(Vector3.forward * 90);
    //        rigid.velocity = new Vector2(enemyLogic.speed * 0.8f, -1);
    //    }
    //    else
    //        rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
    //}

    public void SpawnDelay(float startRange, float endRange)
    {
        spawnDelayran = Random.Range(startRange, endRange);
    }

    public void UpdateLifeIcon(int life, int maxLife)
    {
        for (int i = 0;  i < maxLife - life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 0);
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
        player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
        playerLogic.maxHitDelay = 1;
        Invoke("HitColorAndDelay", playerLogic.maxHitDelay);
    }

    void HitColorAndDelay()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.HitColor();
        playerLogic.maxHitDelay = 0.5f;
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