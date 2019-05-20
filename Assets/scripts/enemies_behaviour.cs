using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemies_behaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    public float speed;

    CapsuleCollider2D self_coll;


    public GameObject bear;
    public int mindistance;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        self_coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         moveDirection = new Vector3(0,0,0);

    }
}
