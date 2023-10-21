using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool isMovable
    {
        get => _isMovable;
        set
        {
            _isMovable = value;
            if (value == false)
            {
                speedModified = 0.0f;
            }
        }
    }
    private bool _isMovable = true;

    public float speedModified
    {
        get => _speedModified;
        set
        {
            _speedModified = value;
            _animator.SetFloat("Speed", value);
        }
    }

    [SerializeField] private float _speed = 3.0f;
    private float _speedModified;

    [SerializeField] private float _speedModifyingPeriod = 1.0f;
    private float _modifyingTimer;
    [Range(0.0f, 1.0f)][SerializeField] private float _stability;

    private Animator _animator;

    public void Finish(int grade)
    {
        isMovable = false;

        switch (grade)
        {
            case 0:
                _animator.Play("Jumping");
                break;
            case 1:
            case 2:
                _animator.Play("Salute");
                break;
            default:
                _animator.Play("KneelDown");
                break;
        }

        PlayManager.instance.RegisterRunnerFinished(this);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayManager.instance.RegisterRunner(this);
    }
        

    // 이동거리 = 속력 * 시간
    // 고정 프레임당 이동거리 = 속력 * 고정 프레임간 시간변화
    private void FixedUpdate()
    {
        if (_isMovable == false)
        {
            return;
        }

        if (_modifyingTimer <= 0.0f)
        {
            speedModified = _speed * Random.Range(_stability, 1.0f);
            _modifyingTimer = _speedModifyingPeriod;
        }
        else
        {
            _modifyingTimer -= Time.fixedDeltaTime;
        }
        //transform.position += Vector3.forward * _speed * Time.fixedDeltaTime;
        transform.Translate(Vector3.forward * _speedModified * Time.fixedDeltaTime);
    }
}
