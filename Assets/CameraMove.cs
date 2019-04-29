using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform playerTransform;
    //public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = new Vector3(12,9,5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 temp = transform.position;
            temp = playerTransform.position;
            transform.position = temp;

        }
        //transform.position = playerObj.transform.position + offset;
    }
}
