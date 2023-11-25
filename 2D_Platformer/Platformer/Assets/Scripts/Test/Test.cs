using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // ���� ������ �� �����͸� �ʱ�ȭ �Ѵ�.
    // ������ ������� �����̳� ���ݷ� ����� �������� �����ͼ� �����Ѵ�.
    private void Awake()
    {
        Debug.Log("�÷��̾� ������ ���� �Ϸ�");
    }

    // ���� ������Ʈ�� Ȱ��ȭ �� �� ȣ��Ǵ� �Լ�
    private void OnEnable()
    {
        Debug.Log("������Ʈ �ʱ�ȭ");
    }

    // ������ �����Ѵ�.
    // ���͸� �����ϰų� ������� �ҷ����� ���� Ȱ��
    void Start()
    {
        Debug.Log("���� ����");
    }

    // ���� �ֱ⸶�� ĳ���� �������� ���������� �����Ѵ�.
    // CPU�� ������ ���� �ʴ´�.
    private void FixedUpdate()
    {
        Debug.Log("ĳ���� ������");
    }

    // ������ �������� ������ �����Ѵ�.
    // CPU�� ������ �޴´�.
    void Update()
    {
        Debug.Log("���� ����ϱ�");
    }

    // ������Ʈ �޼ҵ带 �����ϰ� ���� ������ ����Ʈ�� ������ �ҷ��´�.
    private void LateUpdate()
    {
        Debug.Log("ȿ�� �߻�");
    }

    // ���� ������Ʈ�� ��Ȱ��ȭ �� �� ȣ��Ǵ� �Լ�
    private void OnDisable()
    {
        Debug.Log("������Ʈ �ʱ�ȭ ����");
    }

    // ���Ͱ� ����� �� ȣ��Ǵ� �Լ�
    // ���̾��Ű���� ���� ������Ʈ�� ������ �� ȣ��ȴ�.
    private void OnDestroy()
    {
        Debug.Log("���� �����");
    }
}