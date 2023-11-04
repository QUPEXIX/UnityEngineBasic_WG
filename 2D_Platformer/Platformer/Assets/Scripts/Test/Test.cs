using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 게임 시작할 대 데이터를 초기화 한다.
    // 서버와 통신으로 레벨이나 공격력 등등을 서버에서 가져와서 설정한다.
    private void Awake()
    {
        Debug.Log("플레이어 데이터 설정 완료");
    }

    // 게임 오브젝트가 활성화 될 때 호출되는 함수
    private void OnEnable()
    {
        Debug.Log("오브젝트 초기화");
    }

    // 게임을 실행한다.
    // 몬스터를 스폰하거나 월드맵을 불러오는 등의 활동
    void Start()
    {
        Debug.Log("게임 시작");
    }

    // 일정 주기마다 캐릭터 움직임의 물리연산을 실행한다.
    // CPU의 영향을 받지 않는다.
    private void FixedUpdate()
    {
        Debug.Log("캐릭터 움직임");
    }

    // 게임의 전반적인 로직을 실행한다.
    // CPU의 영향을 받는다.
    void Update()
    {
        Debug.Log("몬스터 사냥하기");
    }

    // 업데이트 메소드를 실행하고 나서 나머지 이펙트나 연산을 불러온다.
    private void LateUpdate()
    {
        Debug.Log("효과 발생");
    }

    // 게임 오브젝트가 비활성화 될 때 호출되는 함수
    private void OnDisable()
    {
        Debug.Log("오브젝트 초기화 해제");
    }

    // 몬스터가 사라질 때 호출되는 함수
    // 하이어라키에서 게임 오브젝트가 삭제될 때 호출된다.
    private void OnDestroy()
    {
        Debug.Log("몬스터 사라짐");
    }
}