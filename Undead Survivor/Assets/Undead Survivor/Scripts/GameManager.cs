using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# Game Controll")]
    public bool isLive;
    public bool isBossBattle;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int totalKill;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# GameObject")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public Text bossStart;
    public GameObject boss;
    public Spawner spawn;
    public Transform uiJoy;
    public GameObject enemyCleaner;
    public GameObject hud;
    public Text totalKillText;

    void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("TotalKill"))
        {
            totalKill = 0;
            PlayerPrefs.SetInt("TotalKill", totalKill);
        }
        else
        {
            totalKill = PlayerPrefs.GetInt("TotalKill");
        }

        totalKillText.text = totalKill.ToString();
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.Instance.PlayBgm(true);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        PlayerPrefs.SetInt("TotalKill", totalKill);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
        AudioManager.Instance.PlayBgm(false);
    }

    public void BossStart()
    {
        StartCoroutine(BossStartRoutine());
    }

    IEnumerator BossStartRoutine()
    {
        hud.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(1, 0.4941176f, 0.9960784f, 1);
        enemyCleaner.SetActive(true);
        bossStart.gameObject.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);

            bossStart.color = new Color(bossStart.color.r, bossStart.color.g, bossStart.color.b, 0);

            yield return new WaitForSeconds(0.5f);

            bossStart.color = new Color(bossStart.color.r, bossStart.color.g, bossStart.color.b, 1);
        }

        enemyCleaner.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        bossStart.gameObject.SetActive(false);

        boss = Instantiate(pool.prefabs[0]);
        boss.transform.position = spawn.transform.GetChild(0).position;
        boss.transform.position += new Vector3(0, 1, 0);
        boss.GetComponent<Enemy>().Init(spawn.spawnDataBoss);
        boss.GetComponent<Enemy>().isBoss = true;
        boss.tag = "Enemy";
        hud.transform.GetChild(0).GetComponent<HUD>().type = HUD.InfoType.BossHealth;
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        PlayerPrefs.SetInt("TotalKill", totalKill);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
        AudioManager.Instance.PlayBgm(false);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime && !isBossBattle)
        {
            gameTime = maxGameTime;
            isBossBattle = true;
            for (int i = 1; i < hud.transform.childCount - 1; i++)
                hud.transform.GetChild(i).gameObject.SetActive(false);
            BossStart();
        }
    }

    public void GetExp()
    {
        if (!isLive || isBossBattle)
            return;

        exp++;
        
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }
}