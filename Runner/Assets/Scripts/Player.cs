using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value == _hp)
                return;

            _hp = value;
            OnHpChanged(value);
        }
    }
    [SerializeField] private float _hp; // serialize: 데이터를 텍스트로 바꾸는 것, deserialize: 텍스트를 데이터로 바꾸는 것

    public float hpMax
    {
        get
        {
            return _hpMax;
        }
    }
    [SerializeField] private float _hpMax = 100;
    public delegate void OnHpChangeHandler(float value);
    public event OnHpChangeHandler OnHpChanged;
}