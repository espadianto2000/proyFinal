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
    public Texture2D cursorNormal;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        cambiarCursorMain();
        irMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && (SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2"))
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
    public void pausa()
    {
        estadoPausa = true;
        Time.timeScale = 0;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void outPausa()
    {
        transform.GetChild(0).gameObject.SetActive(false);
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
    public void cambiarCursorMain()
    {
        Cursor.SetCursor(cursorNormal, new Vector2(24,24), CursorMode.ForceSoftware);
    }
}
