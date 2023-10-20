using System;
using UnityEngine;

public class Operatable<T>
{
    public T value;

    public static Operatable<T> operator +(Operatable<T> a, Operatable<T> b)
    {
        return a;
    }
}


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
            onHpChanged(value);
        }
    }
    [SerializeField] private float _hp; // Serialize : 데이터를 텍스트로, Deserialize : 텍스트를 데이터로

    public float hpMax
    {
        get
        {
            return _hpMax;
        }
    }
    [SerializeField] private float _hpMax = 100;
    public delegate void OnHpChangedHandler(float value);
    //public event OnHpChangedHandler onHpChanged;
    public event Action<float> onHpChanged;

    // Action 대리자
    // 파라미터를 0~ 16개 까지 받을 수 있는 
    // void 를 반환하는 형태의 대리자.
    public Action<int, float, string> action;

    // Func 대리자
    // 파라미터를 0~ 16개 까지 받을 수 있는
    // 제네릭타입을 반환하는 형태의 대리자.
    public Func<int, float, string> func;

    // Predicate 대리자
    // 파라미터 1개 받고,
    // bool 타입 반환하는 형태의 대리자.
    // 어떤 아이템의 match 조건을 검사할때 사용함. (자료구조에서 특정 자료 탐색을 해야할때 주로 씀)
    public Predicate<int> match;


    // Generic 
    // 어떤 타입을 일반화하는 사용자정의 서식
    
    // where 한정자
    // Generic타입이 어떤 타입으로 공변가능한지 제한을 거는 한정자
    public T Sum<T>(T a, T b)
        where T : Operatable<T>
        => (a + b).value;

    public int Sum(int a, int b)
        => a + b;

    public float Sum(float a, float b)
        => a + b;

    public double Sum(double a, double b)
        => a + b;


    public void DepleteHp(float amout)
    {
        hp -= amout;
    }
}
