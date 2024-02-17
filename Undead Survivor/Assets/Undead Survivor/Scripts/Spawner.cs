using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public SpawnData spawnDataBoss;
    public float[] spawnTimes;
    
    int level;
    int spawnTimeLevel;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive || GameManager.Instance.isBossBattle)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime * spawnData.Length / GameManager.Instance.maxGameTime), spawnData.Length - 1);
        spawnTimeLevel = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime * spawnTimes.Length / GameManager.Instance.maxGameTime), spawnTimes.Length - 1);

        if (timer > spawnTimes[spawnTimeLevel])
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[Random.Range(0, level + 1)]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float size;
    public Color color;
    public float health;
    public float speed;
    public float dmg;
    public Enemy.Patterns[] patterns;
    [Header("0: rangeDmg, 1: rangeDelay, 2: rangeCast\n3: bulletSpeed 4:bulletSize 5: bulletDelay 6: bulletLocation\n7: rushDmg, 8: rushDelay, 9: rushCastOut, 10: rushCastIn, 11: rushSpeed")]
    public float[] patternVars = new float[12];
}