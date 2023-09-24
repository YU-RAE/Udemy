using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 5;
    float go = 0.1f;
    TextMesh scoreOutput;
    TextMesh heart;
    int score = 0;
    int h = 3;
    

    public float lowWarn = -4;
    public float jumpBoost = 2.5f;


    void Start()
    {
        scoreOutput = GameObject.Find(name: "Score1").GetComponent<TextMesh>();
        rb = gameObject.GetComponent<Rigidbody>();
        go = 0.5f;
        heart = GameObject.Find(name: "Heart").GetComponent<TextMesh>();

    }

    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * go, 0);
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            if(transform.position.y < lowWarn)
            {
                rb.AddForce(Vector3.up * jumpBoost , ForceMode.Impulse); // ���� �ν�Ʈ�� �޴� �������� Velocity ��� AddForce ����غ�
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        h -= 1;
        heart.text = "���� : " + h;
        if ( h < 1 )
        {
            SceneManager.LoadScene("gameOver");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            addScore(10);
        }

    }

    // ���� ���ϱ�
    public void addScore(int s)
    {
        score += s;
        scoreOutput.text = "���� : " + score;
    }
}
