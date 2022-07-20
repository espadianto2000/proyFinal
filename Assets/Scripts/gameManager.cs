using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public bool estadoPausa=false;
    public int dinero = 0;
    public int nivelVidaExtra = 0;
    public int nivelVelocidadExtra = 0;
    public int nivelDanoExtra = 0;
    public int nivelCritExtra = 0;
    public int nivelExpExtra = 0;
    public int nivelPuntosExtra = 0;
    public int nivelDineroExtra = 0;
    public int nivelSpawnVida = 0;
    public int nivelCuracionExtra = 0;
    public int nivelVelocidadAtaqueExtra = 0;
    public bool desbloquearPersonaje2 = false;
    public bool desbloquearUlti = false;
    public float highScore=0;
    public Texture2D cursorNormal;
    public int[] statsArr;
    public GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        statsArr = new int[10] { 0,0,0,0,0,0,0,0,0,0};
        DontDestroyOnLoad(gameObject);
        cambiarCursorMain();
        irMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && (SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2"))
        {
            if((Time.timeScale == 0 && estadoPausa) || (Time.timeScale == 1 && !estadoPausa))
            {
                if (estadoPausa)
                {
                    outPausa();
                }
                else
                {
                    pausa();
                }
            }
        }
    }
    public void pausa()
    {
        estadoPausa = true;
        Time.timeScale = 0;
        menuPausa.SetActive(true);
    }
    public void outPausa()
    {
        menuPausa.SetActive(false);
        estadoPausa = false;
        Time.timeScale = 1;
    }
    public void reiniciarPartida()
    {
        if(SceneManager.GetActiveScene().name == "jugador1")
        {
            SceneManager.LoadScene("jugador1", LoadSceneMode.Single);
        }else if(SceneManager.GetActiveScene().name == "jugador2")
        {
            SceneManager.LoadScene("jugador2", LoadSceneMode.Single);
        }
    }
    public void empezarPartida(int personaje)
    {
        if(personaje == 1)
        {
            SceneManager.LoadScene("jugador1", LoadSceneMode.Single);
        }
        if(personaje == 2)
        {
            SceneManager.LoadScene("jugador2", LoadSceneMode.Single);
        }
        
    }
    public void irMenuPrincipal()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
    public void acabarPartida()
    {
        //GameObject.Find("salir").SetActive(false);
        GameObject.Find("Heroe").GetComponent<PlayerController>().vidaActual = -1;
    }
    public void cambiarCursorMain()
    {
        Cursor.SetCursor(cursorNormal, new Vector2(24,24), CursorMode.ForceSoftware);
    }
    public void desbloquear(int orden)
    {
        switch (orden)
        {
            case 0:
                if (dinero >= 2500)
                {
                    desbloquearPersonaje2 = true;
                    dinero -= 2500;
                }
                
                break;
            case 1:
                if(dinero >= 1000)
                {
                    desbloquearUlti = true;
                    dinero -= 1000;
                }
                
                break;
        }
    }
    public void mejorar(int orden)
    {
        switch (orden)
        {
            case 0:
                if(dinero>=(100+ 100 * (nivelVidaExtra * nivelVidaExtra))){
                    dinero -= 100 + 100 * (nivelVidaExtra * nivelVidaExtra);
                    nivelVidaExtra++;
                    statsArr[orden] = nivelVidaExtra;
                }
                break;
            case 1:
                if (dinero >= (100 + 100 * (nivelDanoExtra * nivelDanoExtra)))
                {
                    dinero -= 100 + 100 * (nivelDanoExtra * nivelDanoExtra);
                    nivelDanoExtra++;
                    statsArr[orden] = nivelDanoExtra;
                }
                    
                break;
            case 2:
                if (dinero >= (100 + 100 * (nivelVelocidadExtra * nivelVelocidadExtra)))
                {
                    dinero -= 100 + 100 * (nivelVelocidadExtra * nivelVelocidadExtra);
                    nivelVelocidadExtra++;
                    statsArr[orden] = nivelVelocidadExtra;
                }
                    
                break;
            case 3:
                if (dinero >= (100 + 100 * (nivelCritExtra * nivelCritExtra)))
                {
                    dinero -= 100 + 100 * (nivelCritExtra * nivelCritExtra);
                    nivelCritExtra++;
                    statsArr[orden] = nivelCritExtra;
                }
                    
                break;
            case 4:
                if (dinero >= (100 + 100 * (nivelExpExtra * nivelExpExtra)))
                {
                    dinero -= 100 + 100 * (nivelExpExtra * nivelExpExtra);
                    nivelExpExtra++;
                    statsArr[orden] = nivelExpExtra;
                }
                    
                break;
            case 5:
                if (dinero >= (100 + 100 * (nivelPuntosExtra * nivelPuntosExtra)))
                {
                    dinero -= 100 + 100 * (nivelPuntosExtra * nivelPuntosExtra);
                    nivelPuntosExtra++;
                    statsArr[orden] = nivelPuntosExtra;
                }
                    
                break;
            case 6:
                if (dinero >= (100 + 100 * (nivelDineroExtra * nivelDineroExtra)))
                {
                    dinero -= 100 + 100 * (nivelDineroExtra * nivelDineroExtra);
                    nivelDineroExtra++;
                    statsArr[orden] = nivelDineroExtra;
                }
                    
                break;
            case 7:
                if (dinero >= (100 + 100 * (nivelSpawnVida * nivelSpawnVida)))
                {
                    dinero -= 100 + 100 * (nivelSpawnVida * nivelSpawnVida);
                    nivelSpawnVida++;
                    statsArr[orden] = nivelSpawnVida;
                }
                    
                break;
            case 8:
                if (dinero >= (100 + 100 * (nivelCuracionExtra * nivelCuracionExtra)))
                {
                    dinero -= 100 + 100 * (nivelCuracionExtra * nivelCuracionExtra);
                    nivelCuracionExtra++;
                    statsArr[orden] = nivelCuracionExtra;
                }
                    
                break;
            case 9:
                if (dinero >= (100 + 100 * (nivelVelocidadAtaqueExtra * nivelVelocidadAtaqueExtra)))
                {   
                    dinero -= 100 + 100 * (nivelVelocidadAtaqueExtra * nivelVelocidadAtaqueExtra);
                    nivelVelocidadAtaqueExtra++;
                    statsArr[orden] = nivelVelocidadAtaqueExtra;
                }
                    
                break;
        }
    }
}
