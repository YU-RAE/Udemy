using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] wallPrefab;
    public GameObject dropPrefab;
    public float interval = 2.5f; // 일정 시간마다
    public float range = 3;
    float term;

    void Start()
    {
        term = interval; // 시작부터 벽이 하나 나오기 위해
    }

    void Update()
    {
        term += Time.deltaTime;
        if (term >= interval)
        {
            Vector3 pos = transform.position;
            pos.y += Random.Range(-range, range);
            int wallType = Random.Range(0, wallPrefab.Length);
            Instantiate(wallPrefab[wallType], pos, transform.rotation); // 실시간으로 wallPrefab을 만들어준다. 
            if (Random.Range(0, 5) == 0) // 50%의 확률로 떨어지는 장애물 생성
            {
                Instantiate(dropPrefab);
            }
            
            term -= interval;
        }
    }
}
