using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int penguinsn;
    Animator animator;
    Vector2 target;
    CircleCollider2D self_coll;
    public Sprite water;
    public Sprite ice;
    public Sprite cracket_ice_1;
    public Sprite cracket_ice_2;
    public Tilemap tileMap;

    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text restartOverText;
    public Text penguins;
    public Text clock;

    public ParticleSystem vision;
    public List<GameObject> allies;

    private float invunerability;
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = transform.position;
        self_coll = GetComponent<CircleCollider2D>();
        penguinsn = 0;
        invunerability = 0;
        allies = new List<GameObject>();
    }
    //
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            target = ray.origin;
        }
        if (invunerability > 0) {
            invunerability -= Time.deltaTime;
            //Debug.Log(invunerability);
        }
    }
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
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
        MoveBody(rb2d, transform.position, target, Time.deltaTime*speed);
    }
    //
    void MoveBody(Rigidbody2D body, Vector2 from, Vector2 to, float time)
    {
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
            gameOver();
        }
    }
    // 
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
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag != "ally" && coll.tag != "Player" && coll.tag!="safeplace" && coll.tag!="enemy") // si no agua
        {
            Debug.Log(coll.tag);
            Debug.Log(coll.name);
            gameOver();
        }
        if (coll.tag == "safeplace")
        {
            Debug.Log("CHECKPOINT");
            safeplace();
        }
    }
    //COLLIDING WITH AN A PENGUIN, IT WILL ATTACh THE PENGUIN TO THE PLAYER GAMEOBJECT.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ally")
        {
           
                allies.Add(collision.gameObject);
                collision.gameObject.transform.position = transform.position;
                collision.gameObject.transform.parent = transform;
                penguinsn++;
                actualizarHud(penguinsn);
                update_vision(true);
                collision.tag = "Player";
            
           /* else if (slot2.transform.childCount <=0 ) {
                collision.gameObject.transform.position = slot2.transform.position;
                collision.gameObject.transform.parent = slot2.transform;
                penguinsn++;
                actualizarHud(penguinsn);
                update_vision(true);
                collision.tag = "Player";
            }*/
        }
    }
    //DESTROY THE PLAYER GAMEOBJECT AND DISPLAY GAMEOVER MESSAGE
    void gameOver() {
        Destroy(gameObject);
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over";
        restartOverText.text = "Restart Level";
        clock.GetComponent<reloj>().pausar() ;
    }
    void winGame()
    {
        Destroy(gameObject);
        gameOverPanel.SetActive(true);
        gameOverText.text = "You Win!!";
        restartOverText.text = "Start Over";
        clock.GetComponent<reloj>().pausar();
    }
    //UPDATE HUD WITH THE NUMBER ON PENGUINS
    void actualizarHud(int num) {
        penguins.text = "x " + (num+1);
    }
    //TAKE DAMAGE WHEN COLLIDING WITH A ENEMY, IF THERE ARE NO PENGUINS PLAYER DIE.
    void takeDmg() {
        if (penguinsn<=0 && invunerability<=0)
            gameOver();
        else if(invunerability<=0)
        {
            penguinsn--;
            actualizarHud(penguinsn);
            update_vision(false);
            // if(slot1.transform.childCount==1)
            //Destroy(slot1.transform.GetChild(0).gameObject);
            //else if(slot2.transform.childCount==1)
            //Destroy(slot2.transform.GetChild(0).gameObject);
            Destroy(allies[allies.Count-1]);
            allies.RemoveAt(allies.Count - 1);
            invunerability = 2f; // 3 segundos de invunerablidad al sufrir dano
        }
    }
    // COLLISION WITH ENEMY
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
            takeDmg();
        Debug.Log("collision con enemigo");

        if (collision.gameObject.tag == "Player")
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
    // UPDATE RADIUS OF VISION, TRUE = INCREASE, FALSE = DECREASE
    void update_vision(bool positive) {
        var shape = vision.shape;
        if(positive)
            shape.radius = shape.radius + 0.5f;
        else
            shape.radius = shape.radius - 0.5f;
    }
    // SAFEPLACE CODE
    void safeplace() {
        Debug.Log(SceneManager.GetActiveScene().buildIndex + " == " +(SceneManager.sceneCountInBuildSettings - 1));
        if (SceneManager.GetActiveScene().buildIndex != (SceneManager.sceneCountInBuildSettings - 1 )) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else
        {
            winGame();
        }
    }
}
