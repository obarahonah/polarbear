using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_radius : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public GameObject player;
    Vector2 target;
    public float speed = 1.4f;
    public bool goBack;

    
    void Start()
    {
        
        goBack = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position,enemy.transform.position ) < 0.5) {
            enemy.GetComponent<enemies_behaviour>().centeredPosition = true;
            goBack = false;
        }
        if (goBack || enemy.GetComponent<enemies_behaviour>().aggro >0) {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, this.transform.position, speed * Time.deltaTime);
        }
        if (enemy.GetComponent<enemies_behaviour>().attackMode == true && enemy.GetComponent<enemies_behaviour>().aggro <=0) {
            if (player != null) {
                target = player.transform.position;
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, speed * Time.deltaTime);
            }
            
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            enemy.GetComponent<enemies_behaviour>().attackMode = true;
            goBack = false;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.GetComponent<enemies_behaviour>().attackMode = false;
            goBack = true;
        }
    }

}
