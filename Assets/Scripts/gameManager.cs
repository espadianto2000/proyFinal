using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class gameManager : MonoBehaviour
{
    public bool LogeadoGPS = false;
    public string userID = "";
    public bool premium = false;
    public static gameManager instance=null;
    private gameData gd;
    public bool estadoPausa=false;
    [Header("Atributos comprados")]
    public int gems = 0;
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
    [Header("")]
    public Texture2D cursorNormal;
    public int[] statsArr;
    public GameObject menuPausa;
    public AudioSource musica;
    public AudioSource audioNivel;
    public AudioSource audioClick;
    private bool iniciado = false;
    private void Awake()
    {
        if (gameManager.instance == null)
        {
            gameManager.instance = this;
            DontDestroyOnLoad(this);
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void init()
    {
        try
        {
            cargar();
        }
        catch
        {

        }
        inicio();
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
        statsArr[9] = nivelVelocidadAtaqueExtra;
        cambiarCursorMain();
    }

    void inicio()
    {
        StartCoroutine(cargaInicial());
    }

    IEnumerator cargaInicial()
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            //Debug.Log(asyncLoadLevel.progress);
            GameObject.Find("carga").GetComponent<Text>().text = ((Mathf.Round(asyncLoadLevel.progress * 10000)) / 100f) + "%";
            GameObject.Find("Slider").GetComponent<Slider>().value = asyncLoadLevel.progress;
            yield return null;
        }
    }

    void Update()
    {
        if (!iniciado)
        {
            //PlayGamesPlatform.Activate();
            //PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
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

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            LogeadoGPS = true;
            userID = PlayGamesPlatform.Instance.localUser.id;
            Debug.Log("se logea");
            Debug.Log("callback de autenticacion");
            Invoke("init", 1f);
            iniciado = true;
        }
        else
        {
            Debug.Log("continua sin logearse");
            LogeadoGPS = false;
            userID = "guest";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            Debug.Log("callback de autenticacion");
            Invoke("init", 1f);
            iniciado = true;
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
        if (SystemInfo.deviceType == DeviceType.Handheld && SceneManager.GetActiveScene().name != "menu")
        {
            
            Time.timeScale = 0;
            GameObject.Find("Heroe").GetComponent<PlayerController>().fueraPausa = true;
        }else Time.timeScale = 1;
    }
    public void outPausa2()
    {
        menuPausa.SetActive(false);
        estadoPausa = false;
        if (WaveManager.instance.mostrarAnuncio && !premium)
        {
            //WaveManager.instance.mostrarAnuncio = false;
            //mostrar anuncio
            LogicaAds.instance.MostrarInters();
            Debug.Log("se muestra anuncio");
            //AQUIIIIIIII//
            ///////
            ///////
            ///////
        }
        Time.timeScale = 1;
    }
    public void outPass()
    {
        menuPausa.SetActive(false);
        estadoPausa = false;
        if (SystemInfo.deviceType == DeviceType.Handheld && SceneManager.GetActiveScene().name != "menu")
        {
            if (WaveManager.instance.mostrarAnuncio && !premium)
            {
                //WaveManager.instance.mostrarAnuncio = false;
                //mostrar anuncio
                LogicaAds.instance.MostrarInters();
                Debug.Log("se muestra anuncio");
                //AQUIIIIIIII//
                ///////
                ///////
                ///////
            }

            Time.timeScale = 0;
            GameObject.Find("Heroe").GetComponent<PlayerController>().fueraPausa = true;
        }
        else Time.timeScale = 1;
        
    }
    public void forceOutPausa()
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
            Time.timeScale = 1;
        }else if(SceneManager.GetActiveScene().name == "jugador2")
        {
            SceneManager.LoadScene("jugador2", LoadSceneMode.Single);
            Time.timeScale = 1;
        }
    }
    public void empezarPartida(int personaje)
    {
        musicaManager.instance.cambiarAJugador();
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
        if (musicaManager.instance != null) { musicaManager.instance.cambiarAMenu(); }
        SceneManager.LoadSceneAsync("menu", LoadSceneMode.Single);
        
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
                if (dinero >= 5000)
                {
                    desbloquearPersonaje2 = true;
                    dinero -= 5000;
                }
                
                break;
            case 1:
                if(dinero >= 4000)
                {
                    desbloquearUlti = true;
                    dinero -= 4000;
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
        guardar();
    }
    public void guardar()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{userID}.ub";
        FileStream stream = new FileStream(path, FileMode.Create);
        //
        gd = new gameData(gems, dinero, nivelVidaExtra, nivelVelocidadExtra, nivelDanoExtra, nivelCritExtra, nivelExpExtra, nivelPuntosExtra, nivelDineroExtra, nivelSpawnVida, nivelCuracionExtra, nivelVelocidadAtaqueExtra, desbloquearPersonaje2, desbloquearUlti, highScore, premium);
        //
        formatter.Serialize(stream, gd);
        stream.Close();

        /*string json = JsonUtility.ToJson(gd);
        File.Delete(persistentPath);
        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);*/
    }
    public gameData getGameData()
    {
        return new gameData(gems, dinero, nivelVidaExtra, nivelVelocidadExtra, nivelDanoExtra, nivelCritExtra, nivelExpExtra, nivelPuntosExtra, nivelDineroExtra, nivelSpawnVida, nivelCuracionExtra, nivelVelocidadAtaqueExtra, desbloquearPersonaje2, desbloquearUlti, highScore, premium);
    }
    public void cargar()
    {
        string path = Application.persistentDataPath + $"/{userID}.ub";
        gameData data = null;
        if (File.Exists(path))
        {
            Debug.Log("existe archivo");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log(path);
            data = formatter.Deserialize(stream) as gameData;
            stream.Close();
            Debug.Log("cargado correctamente");
        }
        else
        {
            Debug.Log("No data");
        }

        /*using StreamReader rd = new StreamReader(persistentPath);
        string json = rd.ReadToEnd();

        gd = JsonUtility.FromJson<gameData>(json);*/
        gd = data;
        gems = data.gems;
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
        premium = gd.premium;
    }
    public void cargaExterna(gameData gd)
    {
        gems = gd.gems;
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
        premium = gd.premium;
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
        statsArr[9] = nivelVelocidadAtaqueExtra;
        guardar();
    }

    
}
