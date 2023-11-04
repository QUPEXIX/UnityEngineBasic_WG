using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallel : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget; // 플레이어 Transform

    Vector2 startingPosition; // 현재 Transform의 Position

    float startingZ; // -1.2, -0.6, -0.24 이 값하고 플레이어 포지션의 Z값 차이

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z - followTarget.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라와 현재 나의 시작 위치의 차이 Vector2
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;

        // 따라 다닐 캐릭터(플레이어)와 현 오브젝트의 z축 거리값
        float zDistanceFromTarget = transform.position.z - followTarget.transform.position.z;

        // 삼항 연산자
        // int a = (조건) ? (참) : (거짓)
        float clippingPlane = ((cam.transform.position.z) + zDistanceFromTarget) > 0 ?
            cam.farClipPlane : cam.nearClipPlane;

        // -2, 2 = 2
        float parallelFactore = Mathf.Abs(zDistanceFromTarget) / clippingPlane;

        Vector2 newPosition = startingPosition + camMoveSinceStart / parallelFactore;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
