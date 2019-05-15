using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penguin_behaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    public float speed;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent!= null)
        {
            if (gameObject.transform.parent.tag == "ally")
            {

                //gameObject.transform.position = gameObject.transform.parent.position;
                MoveBody(rb2d, transform.position, gameObject.transform.parent.position, Time.deltaTime * speed);

            }
        }

    }

    void MoveBody(Rigidbody2D body, Vector2 from, Vector2 to, float time)
    {
        //body.MovePosition(Vector2.Lerp(from, to, time));
        body.MovePosition(Vector2.MoveTowards(from, to, time));
       
    }
}
