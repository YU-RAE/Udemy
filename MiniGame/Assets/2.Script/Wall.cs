using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    public float speed = -5;
    Player player;
    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>(); // player라는 변수를 만들어 "Player"라는 게임 오브젝트에서 Player라는 컴포넌트를 가져와 변수에 넣는다.

    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

}
