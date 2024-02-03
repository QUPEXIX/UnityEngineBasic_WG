using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
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
        if (!GameManager.Instance.isLive)
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
    public Color color;
    public float health;
    public float speed;
    public float dmg;
    public float rangeDelay;
}