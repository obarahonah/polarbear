using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Se observa en filse-buildSetings: se observa numero de indice a la derecha
    }

    public void buttonExit()
    {
        Application.Quit(); //En unity no se observa el cambio
        Debug.Log("salir");
    }

    public void buttonAbout()
    {
        Application.OpenURL("https://crbenjaminblanco.github.io/GameDesign/"); //En unity no se observa el cambio
        Debug.Log("Ir a la URL");
    }


}
