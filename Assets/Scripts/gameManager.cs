using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameData
{
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
    public float highScore = 0;
    public gameData(int din, int vida, int vel, int dano, int crit, int exp, int pts, int dinex, int spvida, int cur, int velat, bool pj2, bool ulti, float hs)
    {
        this.dinero = din;
        this.nivelSpawnVida = vida;
        this.nivelVelocidadExtra = vel;
        this.nivelDanoExtra = dano;
        this.nivelCritExtra = crit;
        this.nivelExpExtra = exp;
        this.nivelPuntosExtra = pts;
        this.nivelDineroExtra = dinex;
        this.nivelSpawnVida = spvida;
        this.nivelCuracionExtra = cur;
        this.nivelVelocidadAtaqueExtra = velat;
        this.desbloquearPersonaje2 = pj2;
        this.desbloquearUlti = ulti;
        this.highScore = hs;
    }
}

public class gameManager : MonoBehaviour
{
    public gameData gd;
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
    public musicaManager mm;
    public AudioSource audioNivel;

    private string path = "";
    private string persistentPath="";

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "saveData.json";
        persistentPath = Application.persistentDataPath+Path.AltDirectorySeparatorChar+"saveData.json";
        try
        {
            cargar();
            
        }
        catch
        {
        }
        statsArr = new int[10];
        statsArr[0] = nivelVidaExtra;
        statsArr[1] = nivelDanoExtra;
        statsArr[2] = nivelVelocidadExtra;
        statsArr[3] = nivelCritExtra;
        statsArr[4] = nivelExpExtra;
        statsArr[5] = nivelPuntosExtra;
        statsArr[6] = nivelDineroExtra;
        statsArr[7] = nivelSpawnVida;
        statsArr[8] = nivelCuracionExtra;
        statsArr[9] = nivelVelocidadExtra;
        DontDestroyOnLoad(gameObject);
        cambiarCursorMain();
        irMenuPrincipal();
        audioNivel = GameObject.Find("AudioNivel").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            dinero += 10000;
        }
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
        mm.cambiarAJugador();
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
        if (mm != null) { mm.cambiarAMenu(); }
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
                if (dinero >= 1500)
                {
                    desbloquearPersonaje2 = true;
                    dinero -= 1500;
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
                    audioNivel.Play();
                }
                break;
            case 1:
                if (dinero >= (100 + 100 * (nivelDanoExtra * nivelDanoExtra)))
                {
                    dinero -= 100 + 100 * (nivelDanoExtra * nivelDanoExtra);
                    nivelDanoExtra++;
                    statsArr[orden] = nivelDanoExtra;
                    audioNivel.Play();
                }

                break;
            case 2:
                if (dinero >= (100 + 100 * (nivelVelocidadExtra * nivelVelocidadExtra)))
                {
                    dinero -= 100 + 100 * (nivelVelocidadExtra * nivelVelocidadExtra);
                    nivelVelocidadExtra++;
                    statsArr[orden] = nivelVelocidadExtra;
                    audioNivel.Play();
                }

                break;
            case 3:
                if (dinero >= (100 + 100 * (nivelCritExtra * nivelCritExtra)))
                {
                    dinero -= 100 + 100 * (nivelCritExtra * nivelCritExtra);
                    nivelCritExtra++;
                    statsArr[orden] = nivelCritExtra;
                    audioNivel.Play();
                }

                break;
            case 4:
                if (dinero >= (100 + 100 * (nivelExpExtra * nivelExpExtra)))
                {
                    dinero -= 100 + 100 * (nivelExpExtra * nivelExpExtra);
                    nivelExpExtra++;
                    statsArr[orden] = nivelExpExtra;
                    audioNivel.Play();
                }

                break;
            case 5:
                if (dinero >= (100 + 100 * (nivelPuntosExtra * nivelPuntosExtra)))
                {
                    dinero -= 100 + 100 * (nivelPuntosExtra * nivelPuntosExtra);
                    nivelPuntosExtra++;
                    statsArr[orden] = nivelPuntosExtra;
                    audioNivel.Play();
                }

                break;
            case 6:
                if (dinero >= (100 + 100 * (nivelDineroExtra * nivelDineroExtra)))
                {
                    dinero -= 100 + 100 * (nivelDineroExtra * nivelDineroExtra);
                    nivelDineroExtra++;
                    statsArr[orden] = nivelDineroExtra;
                    audioNivel.Play();
                }

                break;
            case 7:
                if (dinero >= (100 + 100 * (nivelSpawnVida * nivelSpawnVida)))
                {
                    dinero -= 100 + 100 * (nivelSpawnVida * nivelSpawnVida);
                    nivelSpawnVida++;
                    statsArr[orden] = nivelSpawnVida;
                    audioNivel.Play();
                }

                break;
            case 8:
                if (dinero >= (100 + 100 * (nivelCuracionExtra * nivelCuracionExtra)))
                {
                    dinero -= 100 + 100 * (nivelCuracionExtra * nivelCuracionExtra);
                    nivelCuracionExtra++;
                    statsArr[orden] = nivelCuracionExtra;
                    audioNivel.Play();
                }

                break;
            case 9:
                if (dinero >= (100 + 100 * (nivelVelocidadAtaqueExtra * nivelVelocidadAtaqueExtra)))
                {   
                    dinero -= 100 + 100 * (nivelVelocidadAtaqueExtra * nivelVelocidadAtaqueExtra);
                    nivelVelocidadAtaqueExtra++;
                    statsArr[orden] = nivelVelocidadAtaqueExtra;
                    audioNivel.Play();
                }

                break;
        }
    }
    public void guardar()
    {
        gd = new gameData(dinero, nivelVidaExtra, nivelVelocidadExtra, nivelDanoExtra, nivelCritExtra, nivelExpExtra, nivelPuntosExtra, nivelDineroExtra, nivelSpawnVida, nivelCuracionExtra, nivelVelocidadAtaqueExtra, desbloquearPersonaje2, desbloquearUlti, highScore);
        string json = JsonUtility.ToJson(gd);
        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
    }
    public void cargar()
    {
        using StreamReader rd = new StreamReader(persistentPath);
        string json = rd.ReadToEnd();

        gd = JsonUtility.FromJson<gameData>(json);
        dinero = gd.dinero;
        nivelVidaExtra = gd.nivelVidaExtra;
        nivelDanoExtra = gd.nivelDanoExtra;
        nivelVelocidadExtra = gd.nivelVelocidadExtra;
        nivelDanoExtra = gd.nivelDanoExtra;
        nivelCritExtra = gd.nivelCritExtra;
        nivelExpExtra = gd.nivelExpExtra;
        nivelPuntosExtra = gd.nivelPuntosExtra;
        nivelDineroExtra = gd.nivelDineroExtra;
        nivelSpawnVida = gd.nivelSpawnVida;
        nivelCuracionExtra = gd.nivelCuracionExtra;
        nivelVelocidadAtaqueExtra = gd.nivelVelocidadAtaqueExtra;
        desbloquearPersonaje2 = gd.desbloquearPersonaje2;
        desbloquearUlti = gd.desbloquearUlti;
        highScore = gd.highScore;
    }
}
