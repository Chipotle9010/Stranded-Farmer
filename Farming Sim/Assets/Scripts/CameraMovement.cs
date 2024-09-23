using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform target;

    //---------------------------------------------------------------------------------------------

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //---------------------------------------------------------------------------------------------

    private void LateUpdate()
    {
        Vector3 position = target.position;
        position.z = transform.position.z;

        transform.position = position;
    }

}
