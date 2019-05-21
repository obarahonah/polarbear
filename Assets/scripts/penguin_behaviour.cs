using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class penguin_behaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    public float speed;

    CapsuleCollider2D self_coll;


    public GameObject bear;
    public int mindistance;
    private List<Vector3> storedPositions;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        self_coll = GetComponent<CapsuleCollider2D>();
        storedPositions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.transform.parent!= null)
        {
            if (gameObject.transform.parent.tag == "ally")
            {
                // codigo original : https://forum.unity.com/threads/how-do-you-make-an-object-follow-your-exact-movement-but-delayed.512787/

                if (storedPositions.Count == 0)
                {
                    Debug.Log("blank list");
                    storedPositions.Add(bear.transform.position); //store the players currect position
                    return;
                }
                else if (storedPositions[storedPositions.Count - 1] != bear.transform.position)
                {

                    storedPositions.Add(bear.transform.position); //store the position every frame
                }

                if (storedPositions.Count > mindistance)
                {
                    MoveBody(rb2d, transform.position, storedPositions[0], Time.deltaTime * speed);
                    //transform.position = storedPositions[0]; //move
                    storedPositions.RemoveAt(0); //delete the position that player just move to
                }


            }
        }


    }

    void MoveBody(Rigidbody2D body, Vector2 from, Vector2 to, float time)
    {
        body.MovePosition(Vector2.Lerp(from, to, time));
        //body.MovePosition(Vector2.MoveTowards(from, to, time));
       
    }
}
