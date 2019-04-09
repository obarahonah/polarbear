using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.

    private Transform rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    Animator animator;
    Vector2 target;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        target = transform.position;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray);

        if (Input.GetButtonDown("Fire1"))
        {
            
                
            target = ray.origin;
            

        }

        if (target.x > transform.position.x)
        {
            animator.SetFloat("walk_left", 0f);
            animator.SetFloat("walk_right", 0.1f);

        }
        if (target.x < transform.position.x)
        {
            animator.SetFloat("walk_right", 0f);
            animator.SetFloat("walk_left", -0.1f);
        }
        if (target.x==transform.position.x && target.y==transform.position.y)
        {
            animator.SetFloat("walk_left", 0f);
            animator.SetFloat("walk_right", 0f);

        }
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }


    
}
