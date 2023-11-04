using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallel : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget; // �÷��̾� Transform

    Vector2 startingPosition; // ���� Transform�� Position

    float startingZ; // -1.2, -0.6, -0.24 �� ���ϰ� �÷��̾� �������� Z�� ����

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z - followTarget.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶�� ���� ���� ���� ��ġ�� ���� Vector2
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;

        // ���� �ٴ� ĳ����(�÷��̾�)�� �� ������Ʈ�� z�� �Ÿ���
        float zDistanceFromTarget = transform.position.z - followTarget.transform.position.z;

        // ���� ������
        // int a = (����) ? (��) : (����)
        float clippingPlane = ((cam.transform.position.z) + zDistanceFromTarget) > 0 ?
            cam.farClipPlane : cam.nearClipPlane;

        // -2, 2 = 2
        float parallelFactore = Mathf.Abs(zDistanceFromTarget) / clippingPlane;

        Vector2 newPosition = startingPosition + camMoveSinceStart / parallelFactore;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
