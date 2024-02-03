using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //�������� ������ ����
    public GameObject[] prefabs;
    public string[] poolTags;

    //Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //������ Ǯ���� ��� �ִ�(��Ȱ��ȭ��) ���� ������Ʈ ����, �߰��ϸ� select ������ �Ҵ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                StartCoroutine(PreventHittingImmediately(select, index));
                break;
            }
        }

        //��Ȱ��ȭ�� ���� ������Ʈ�� ������ ���Ӱ� �����ؼ� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            StartCoroutine(PreventHittingImmediately(select, index));
            pools[index].Add(select);
        }

        return select;
    }

    IEnumerator PreventHittingImmediately(GameObject select, int index)
    {
        yield return new WaitForFixedUpdate();

        select.tag = poolTags[index];
    }
}