using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        tr.position = new Vector3(target.position.x, tr.position.y + 0.5f, target.position.z);
    }

    void Update()
    {
        
    }
}
