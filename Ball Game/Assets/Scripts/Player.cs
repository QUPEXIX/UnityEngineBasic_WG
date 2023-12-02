using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float jumpPower = 50;
    public int score;
    private int count;
    Rigidbody rb;
    new AudioSource audio;
    public GameManager manager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && count > 0)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            count--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            count = 2;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            score++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(score);
        }
        else if (other.tag == "Finish")
        {
            if (score == manager.totalItemCount)
                SceneManager.LoadScene("Scene" + (manager.stage + 1));
            else
                SceneManager.LoadScene("Scene" + manager.stage);
        }
    }
}