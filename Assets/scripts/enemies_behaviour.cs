using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class enemies_behaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb2d;
    public float speed;

    CapsuleCollider2D self_coll;


    public GameObject bear;
    public int mindistance;
    private Vector3 moveDirection = Vector3.zero;

    public Sprite water;
    public Tilemap tileMap;

    public string currentDir = "Down";
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 1f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private Vector2 lastSaveMovementPerSecond;

    private string[] directions = { "Up", "Down", "Right", "Left" };
    private string[][] transitionName = new string[][]{
        new string[] { "UU", "UD", "UR", "UL" },
        new string[] { "DU", "DD", "DR", "DL" },
        new string[] { "RU", "RD", "RR", "RL" },
        new string[] { "LU", "LD", "LR", "LL" }
        };
    private float[][] transitionMatrix = new float[][] {
        new float[] { 0.3f, 0.05f, 0.35f, 0.3f },
        new float[] { 0.05f, 0.15f, 0.4f, 0.4f},
        new float[] { 0.4f, 0.25f, 0.3f, 0.05f},
        new float[] { 0.45f, 0.3f, 0.05f, 0.2f }
        };


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        self_coll = GetComponent<CapsuleCollider2D>();
        latestDirectionChangeTime = 0f;
        CalcuateNewMovementVector(false);
    }

    // Update is called once per frame
    void Update()
    {
        //moveDirection = new Vector3(0,0,0)

        Vector3Int lPos = tileMap.WorldToCell(self_coll.transform.position);
        Tile tile = tileMap.GetTile<Tile>(lPos);
        if (tile.sprite == water)
        {
            Debug.Log("Water");
            Debug.Log(movementPerSecond);
            latestDirectionChangeTime = Time.time;
            CalcuateNewMovementVector(true);
            Debug.Log(movementPerSecond);
            //movementPerSecond *= new Vector2(-1f, -1f);
            //Destroy(gameObject);
            //movementPerSecond = lastSaveMovementPerSecond;
        } 
         else 
        {
            //Debug.Log("Save");
            //movementPerSecond *= new Vector2(-1f, -1f);
            //lastSaveMovementPerSecond = movementPerSecond;
        }

        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            CalcuateNewMovementVector(false);
        }

        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));

    }

    void CalcuateNewMovementVector(bool back)
    {
        float x = 0f;
        float y = 0f;
        string change = "";
        if (!back)
        {
            switch (currentDir)
            {
                case "Up":
                    change = GetNextDirection(transitionName[0], transitionMatrix[0]);
                    break;
                case "Down":
                    change = GetNextDirection(transitionName[1], transitionMatrix[1]);
                    break;
                case "Right":
                    change = GetNextDirection(transitionName[2], transitionMatrix[2]);
                    break;
                case "Left":
                    change = GetNextDirection(transitionName[3], transitionMatrix[3]);
                    break;
            }
        } else
        {
            switch (currentDir)
            {
                case "Up":
                    change = "UD";
                    break;
                case "Down":
                    change = "DU";
                    break;
                case "Right":
                    change = "RL";
                    break;
                case "Left":
                    change = "LR";
                    break;
            }
        }

        switch (change.ToCharArray()[1])
        {
            case 'U':
                x = 0.0f;
                y = 1.0f;
                currentDir = "Up";
                break;
            case 'D':
                x = 0.0f;
                y = -1.0f;
                currentDir = "Down";
                break;
            case 'R':
                x = 1.0f;
                y = 0.0f;
                currentDir = "Right";
                break;
            case 'L':
                x = -1.0f;
                y = 0.0f;
                currentDir = "Left";
                break;
        }
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(x, y).normalized;

        movementPerSecond = movementDirection * speed;
    }

    string GetNextDirection(string[] elements, float[] prob)
    {
        //Random r = new Random();
        float diceRoll = Random.Range(0.0f, 1.0f);

        float cumulative = 0.0f;
        int index = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            cumulative += prob[i];
            if (diceRoll < cumulative)
            {
                index = i;
                break;
            }
        }
        return elements[index];
    }

    void MoveBody(Rigidbody2D body, Vector2 from, Vector2 to, float time)
    {
        body.MovePosition(Vector2.Lerp(from, to, time));
        //body.MovePosition(Vector2.MoveTowards(from, to, time));

    }
}
