using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float curShotDelay;
    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    public int followerIndex;

    void Awake()
    {
        //Queue의 자료 구조는 FIFO(first input, first output / 먼저 들어가면 먼저 나온다)
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    void Watch()
    {
        parentPos.Enqueue(parent.position);

        if (parentPos.Count > followDelay)
            switch (followerIndex)
            {
                case 0:
                    followPos = parentPos.Dequeue() + new Vector3(0.73f, 0.53f, 0);
                    break;
                case 1:
                    followPos = parentPos.Dequeue() + new Vector3(-0.73f, 0.53f, 0);
                    break;
                case 2:
                    followPos = parentPos.Dequeue() + new Vector3(0, 0.86f, 0);
                    break;
            }
            
        else if (parentPos.Count < followDelay)
            followPos = transform.position;
    }

    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet1 = objectManager.MakeObj("BulletFollower");
        bullet1.transform.position = transform.position;
        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        rigid1.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}