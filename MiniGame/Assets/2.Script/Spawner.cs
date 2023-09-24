using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] wallPrefab;
    public GameObject dropPrefab;
    public float interval = 2.5f; // ���� �ð�����
    public float range = 3;
    float term;

    void Start()
    {
        term = interval; // ���ۺ��� ���� �ϳ� ������ ����
    }

    void Update()
    {
        term += Time.deltaTime;
        if (term >= interval)
        {
            Vector3 pos = transform.position;
            pos.y += Random.Range(-range, range);
            int wallType = Random.Range(0, wallPrefab.Length);
            Instantiate(wallPrefab[wallType], pos, transform.rotation); // �ǽð����� wallPrefab�� ������ش�. 
            if (Random.Range(0, 5) == 0) // 50%�� Ȯ���� �������� ��ֹ� ����
            {
                Instantiate(dropPrefab);
            }
            
            term -= interval;
        }
    }
}