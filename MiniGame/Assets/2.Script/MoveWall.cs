using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    GameObject moveWall;
    float go = 0.3f;
    public float speed = -5;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        moveWall = GameObject.Find(name: "MoveWall");
        player = GameObject.Find(name: "Player").GetComponent<Player>(); // player라는 변수를 만들어 "Player"라는 게임 오브젝트에서 Player라는 컴포넌트를 가져와 변수에 넣는다.

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * go, 0);
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
