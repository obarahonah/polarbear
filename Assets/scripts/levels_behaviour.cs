using UnityEngine;
using UnityEngine.SceneManagement;



public class levels_behaviour : MonoBehaviour
{

    public string LevelName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(LevelName);
        }
    }
}
