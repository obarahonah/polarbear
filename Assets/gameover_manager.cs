using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameover_manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Button m_quit, m_restart;
    void Start()
    {
        m_quit.onClick.AddListener(quit_app);
        m_restart.onClick.AddListener(restart_current);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    // reinicia el nivel actual
    void restart_current() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // termina la ejecucion de la aplicacion
    void quit_app() {
        Debug.Log("boton");
        Application.Quit();
    }

}
