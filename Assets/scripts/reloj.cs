using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class reloj : MonoBehaviour
{
    [Tooltip("Tiempo inicial en segundos")]
    public int tiempoInicial; //Tiempo con el que arranca el juego

    [Tooltip("Escala del tiempo")]
    [Range(-10.0f,10.0f)] //Rango
    public float escalaDelTiempo = -1; //Configura que tan rapido y si adelante o hacia atras
    public ParticleSystem vision;
    private Text myText;
    private float tiempoDelFramConTimeScale = 0f;
    private float tiempoMostrarSegundos = 0f;
    private float escalaDeTiempoAlPausar, escalaDeTiempoInicial;
    private bool estaPausado;
    private bool cond;
    private bool timeover;

    // Start is called before the first frame update
    void Start()
    {
        //Establece la escala de tiempo original
        escalaDeTiempoInicial = escalaDelTiempo;

        //Obtiene el text component
        myText = GetComponent<Text>();

        //Inicia la variable que acumula los tiempos de cada frame
        tiempoMostrarSegundos = tiempoInicial;

        actualizarReloj(tiempoInicial);
        timeover = false;
        cond = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!estaPausado)
        {
            //Representa el tiempo en cada frame
            tiempoDelFramConTimeScale = Time.deltaTime * escalaDelTiempo;

            //Acumula el tiempo transcurrido para mostrarlo
            tiempoMostrarSegundos += tiempoDelFramConTimeScale;

            /*Aca se deberia verificar si se le acabo el tiempo */

            actualizarReloj(tiempoMostrarSegundos);

            if (timeover && !cond) {
                var shape = vision.shape;
                shape.radius = 1f;
                cond = true;
                //vision.transform.position.Set(transform.position.x, transform.position.y,-0.5f);

            }

        }
    }

    public void actualizarReloj(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        //Asegurar que el tiempo no sea negativo
        if (tiempoEnSegundos < 0)
        {
            tiempoEnSegundos = 0;
            Debug.Log("TIMEOVER");
            timeover = true;
        }

        //Calcula los minutos y segundos

        minutos = (int)tiempoEnSegundos / 60;
        segundos = (int)tiempoEnSegundos % 60;

        //Crea la string con el formato 00:00
        textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");

        //actializa el  UI
        myText.text = textoDelReloj;

    }

    public void pausar()
    {
        if (!estaPausado)
        {
            estaPausado = true;
            escalaDeTiempoAlPausar = escalaDelTiempo;
            escalaDelTiempo = 0;
        }
    }

    public void continuar()
    {
        if (estaPausado)
        {
            estaPausado = false;
            escalaDelTiempo = escalaDeTiempoAlPausar;
        }
    }

    public void reiniciar()
    {
        estaPausado = true;
        escalaDelTiempo = escalaDeTiempoInicial;
        tiempoMostrarSegundos = tiempoInicial;
        actualizarReloj(tiempoMostrarSegundos);
    }

    /*Basado en el tutorial: https://www.youtube.com/watch?v=itU9_9U3KhE&t=268s */

}
