using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject().AddComponent<PlayManager>();
            return _instance;
        }
    }
    private static PlayManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
    }

    public Runner lead
    {
        get
        {
            if (_runnerFinishedCount > 0)
            {
                return _runnersFinished[0];
            }

            Runner runner = _runners[0];
            for (int i = 1; i < _runners.Length; i++)
            {
                if (runner.transform.position.z < _runners[i].transform.position.z)
                    runner = _runners[i];
            }
            return runner;
        }
    }

    private Runner[] _runners = new Runner[5];
    private Runner[] _runnersFinished = new Runner[5];

    private int _runnerCount;
    private int _runnerFinishedCount;
    [SerializeField] private Transform[] _platforms;

    public void RegisterRunner(Runner runner)
    {
        _runners[_runnerCount] = runner;
        _runnerCount++;
    }

    public void RegisterRunnerFinished(Runner runner)
    {
        _runnersFinished[_runnerFinishedCount] = runner;
        _runnerFinishedCount++;

        if (_runnerFinishedCount >= _runnerCount)
        {
            Invoke("PlaceRunnersOnPlatforms", 3.0f);
        }
    }

    private void PlaceRunnersOnPlatforms()
    {
        for (int i = 0; i < _platforms.Length; i++)
        {
            _runnersFinished[i].transform.position = _platforms[i].position;
        }
    }
}