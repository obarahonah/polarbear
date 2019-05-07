using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    Animator animator;
    Vector2 target;
    BoxCollider2D self_coll;
    public Sprite water;
    public Sprite ice;
    public Sprite cracket_ice_1;
    public Sprite cracket_ice_2;
    public Tilemap tileMap;

    public GameObject gameOverPanel;
    public Text gameOverText;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = transform.position;
        self_coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            target = ray.origin;
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        
        //Debug.Log(ray);

        

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
        // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        MoveBody(rb2d, transform.position, target, Time.deltaTime*speed);
    }



    void MoveBody(Rigidbody2D body, Vector2 from, Vector2 to, float time)
    {
        //body.MovePosition(Vector2.Lerp(from, to, time));
        body.MovePosition(Vector2.MoveTowards(from, to, time));

        Vector3Int lPos = tileMap.WorldToCell(self_coll.transform.position);
        Tile tile = tileMap.GetTile<Tile>(lPos);

        if (tile.sprite == ice)
        {
            Debug.Log(tile.sprite.ToString());
            StartCoroutine(IceBreaker(lPos));
        }

        else if(tile.sprite == water)
        {
            Debug.Log(tile.sprite.ToString());
            Destroy(gameObject);
        }

    }

    public IEnumerator IceBreaker(Vector3Int icePosition)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        yield return new WaitForSeconds(0.4f);
        tile.sprite = cracket_ice_1;
        tileMap.SetTile(icePosition, tile);
        tileMap.RefreshAllTiles();

        
        yield return new WaitForSeconds(0.4f);
        tile.sprite = cracket_ice_2;
        tileMap.SetTile(icePosition, tile);
        tileMap.RefreshAllTiles();
        
        yield return new WaitForSeconds(0.4f);
        tile.sprite = water;
        tileMap.SetTile(icePosition, tile);
        tileMap.RefreshAllTiles();
        //tile.sprite = water;
        //icePosition.y -= 1;//it's one tile off from y

    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag != "ally") // si no es aliado
        {
            Destroy(gameObject);
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Over";
        }
        
    }



}
