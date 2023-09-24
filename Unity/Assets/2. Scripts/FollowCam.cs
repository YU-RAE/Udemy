using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;
    public Vector3 offset;

    void Start()
    {
    }

    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = targetTr.position + offset;
    }
}
