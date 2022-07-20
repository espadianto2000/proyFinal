using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Material origMaterial;
    public Material flashMaterial;
    public bool vulnerable = true;
    public Rigidbody2D rb;
    public gameManager gm;
    public GameObject hmThorns;
    public GameObject menuMuerte;
    public WaveManager wm;
    public GameObject hmHeal;
    [Header("Stats")]
    public float VelocidadBase;
    public float DanoBase;
    public float vidaMax;
    public float vidaActual;
    public bool vivo;
    public float tamanoAtaque;
    public float velocidadAtaque;
    public float probCritico;
    public float porcentajeExp;
    public float porcentajePuntos;
    public float porcentajeDinero;
    public float porcentajeAparicionCorazones;
    public float porcentajeCuraciones;
    public Slider vidaSlider;
    public GameObject dagaPrefab;
    public GameObject cactusPrefab;
    public GameObject bombaPrefab;
    public int nivelVidaMejora = 0;
    public int nivelDanoMejora = 0;
    public int nivelVelocidadMejora = 0;
    public int nivelCriticoMejora = 0;
    public int nivelVelocidadAtaqueMejora = 0;
    public int nivelAlcanceMejora = 0;
    public int nivelXPMejora = 0;
    public int nivelPtsMejora = 0;
    public int nivelDineroMejora = 0;

    [Header("movimiento")]
    public Vector2 destinoAtaque = new Vector2(0,0);
    public Renderer rd;

    [Header("ataque")]
    public GameObject slash;
    public float timerAtaque=0;
    public GameObject flecha;

    [Header("subidaNivel")]
    public float xpNecesaria;
    public float exp;
    public GameObject menuSubidaNivel;
    public Slider xpSlider;

    [Header("Poderes")]
    public bool armaduraEspinas;
    public bool roboVida;
    public bool escudoProtector;
    public float escudoProtectorVida = 50;
    public float delayEscudo;
    public bool escudoInmortal;
    public float delayInmortal;
    private bool activadoInmortal;
    public bool invisibilidad;
    private bool invisibilidadActiva;
    public bool esVisible = true;
    private float inviDelay;
    public bool cactusPoder;
    private bool cactusActivado;
    public float cactusDelay;
    public bool evadirAtaque;
    public float probEvadir;
    public bool dagaPoder;
    private float dagaDelay;
    public float cargaUlti=0;
    public bool ultiUsado = false;
    public bool bombaPoder;
    public bool bombaActivada;
    public float bombaDelay;
    [Header("Nivel Poderes")]
    public int armEspLVL;
    public int roboVidaLVL;
    public int escudoProtLVL;
    public int escudoInmLVL;
    public int invLVL;
    public int cactusLVL;
    public int evasionLVL;
    public int dagaLVL;
    public int bombaLVL;
    void Start()
    {
        if (GameObject.Find("gameManager") != null)
        {
            gm = GameObject.Find("gameManager").GetComponent<gameManager>();
            vidaMax = vidaMax * (1 + (0.04f * gm.nivelVidaExtra));
            DanoBase = DanoBase * (1 + (0.05f * gm.nivelDanoExtra));
            VelocidadBase = VelocidadBase * (1 + (0.06f * gm.nivelVelocidadExtra));
            probCritico = probCritico + (0.04f * gm.nivelCritExtra);
            porcentajeExp = porcentajeExp + (0.05f*gm.nivelExpExtra);
            porcentajePuntos = porcentajePuntos + (0.05f * gm.nivelPuntosExtra);
            porcentajeDinero = porcentajeDinero + (0.05f * gm.nivelDineroExtra);
            porcentajeAparicionCorazones = porcentajeAparicionCorazones + (0.01f * gm.nivelSpawnVida);
            porcentajeCuraciones = porcentajeCuraciones + (0.04f * gm.nivelCuracionExtra);
            velocidadAtaque = velocidadAtaque * (1 - (0.05f * gm.nivelVelocidadAtaqueExtra));
            velocidadAtaque = velocidadAtaque < 0.3f ? 0.3f : velocidadAtaque;
        }
        origMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        vivo = true;
        rd = GetComponentInChildren<Renderer>();
        vidaActual = vidaMax;
        probEvadir = 0;
    }
    void Update()
    {
       
            switch (evasionLVL)
            {
                case 1:
                    probEvadir = 0.3f;
                    break;
                case 2:
                    probEvadir = 0.35f;
                    break;
                case 3:
                    probEvadir = 0.4f;
                    break;
                case 4:
                    probEvadir = 0.45f;
                    break;
                case 5:
                    probEvadir = 0.5f;
                    break;
            }
         
        
        if (!vulnerable & vivo)
        {
            if (transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (ultiUsado)
        {
            cargaUlti -= Time.deltaTime / 5;
            if (cargaUlti <= 0)
            {
                ultiUsado = false;
                GameObject.Find("Ulti").transform.GetChild(1).gameObject.SetActive(false);
                slash.transform.localScale = new Vector3(tamanoAtaque, tamanoAtaque, tamanoAtaque);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && cargaUlti >=1 && !ultiUsado && gm.desbloquearUlti)                               
        {
            ultiUsado = true;
            if(SceneManager.GetActiveScene().name == "jugador1")
            {
                //ulti galahad
                slash.transform.localScale = new Vector3(tamanoAtaque, tamanoAtaque, tamanoAtaque) * 2.5f;
            }
            else
            {
                //ulti beatriz

            }
            
        }
        
        if(vidaActual <= 0 && vivo)
        {
            vivo = false;
            transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 2);
            Invoke("desplegarMenuMuerte",2.5f);
        }
        if (exp >= xpNecesaria)
        {
            exp = exp - xpNecesaria;
            Debug.Log("se sube de nivel");
            xpNecesaria = xpNecesaria * 1.25f;
            //manejar subida de nivel
            menuSubidaNivel.SetActive(true);
            //menuSubidaNivel.transform.GetChild(4).gameObject.SetActive(true);
            menuSubidaNivel.transform.GetChild(4).GetComponent<improvementsManager>().generarMejoras();
            menuSubidaNivel.transform.parent.GetComponent<interfazInGameManager>().updateStats(vidaMax, DanoBase, VelocidadBase, probCritico, velocidadAtaque, tamanoAtaque, porcentajeExp, porcentajePuntos, porcentajeDinero);
            Time.timeScale = 0;
        }
        xpSlider.value = exp / xpNecesaria;
        vidaSlider.value = vidaActual / vidaMax;
            if(escudoProtLVL>=1)
            {
                switch (escudoProtLVL)
                        {
                    case 1:
                        if (escudoProtector && escudoProtectorVida <= 0)
                        {
                            //escudoProtector = false;
                            delayEscudo += Time.deltaTime;
                        }
                        if (delayEscudo >= 60)
                        {
                            escudoProtectorVida = 50;
                            delayEscudo = 0;
                        }break;
                    case 2:
                        if (escudoProtector && escudoProtectorVida <= 0)
                        {
                            //escudoProtector = false;
                            delayEscudo += Time.deltaTime;
                        }
                        if (delayEscudo >= 55)
                        {
                            escudoProtectorVida = 60;
                            delayEscudo = 0;
                        }break;
                    case 3:
                        if (escudoProtector && escudoProtectorVida <= 0)
                        {
                            //escudoProtector = false;
                            delayEscudo += Time.deltaTime;
                        }
                        if (delayEscudo >= 50)
                        {
                            escudoProtectorVida = 70;
                            delayEscudo = 0;
                        } break;
                    case 4:
                        if (escudoProtector && escudoProtectorVida <= 0)
                        {
                            //escudoProtector = false;
                            delayEscudo += Time.deltaTime;
                        }
                        if (delayEscudo >= 45)
                        {
                            escudoProtectorVida = 80;
                            delayEscudo = 0;
                        }break;
                    case 5:
                        if (escudoProtector && escudoProtectorVida <= 0)
                        {
                            //escudoProtector = false;
                            delayEscudo += Time.deltaTime;
                        }
                        if (delayEscudo >= 40)
                        {
                            escudoProtectorVida = 90;
                            delayEscudo = 0;
                        }break;
                }
            
        }
        if(escudoInmLVL>=1)
        {
            switch(escudoInmLVL)
            {
                case 1:
                    if (escudoInmortal && activadoInmortal)
                    {
                        delayInmortal += Time.deltaTime;
                    }
                    if (delayInmortal >= 180)
                    {
                        delayInmortal = 0;
                        activadoInmortal = false;
                    }break;
                case 2:
                    if (escudoInmortal && activadoInmortal)
                    {
                        delayInmortal += Time.deltaTime;
                    }
                    if (delayInmortal >= 170)
                    {
                        delayInmortal = 0;
                        activadoInmortal = false;
                    }break;
                case 3:
                    if (escudoInmortal && activadoInmortal)
                    {
                        delayInmortal += Time.deltaTime;
                    }
                    if (delayInmortal >= 160)
                    {
                        delayInmortal = 0;
                        activadoInmortal = false;
                    }break;
                case 4:
                    if (escudoInmortal && activadoInmortal)
                    {
                        delayInmortal += Time.deltaTime;
                    }
                    if (delayInmortal >= 150)
                    {
                        delayInmortal = 0;
                        activadoInmortal = false;
                    }break;
                case 5:
                    if (escudoInmortal && activadoInmortal)
                    {
                        delayInmortal += Time.deltaTime;
                    }
                    if (delayInmortal >= 140)
                    {
                        delayInmortal = 0;
                        activadoInmortal = false;
                    }break;


            }

        }
        if(invLVL>=1)
        {
            switch(invLVL)
            {
                case 1:
                    if (invisibilidad && Input.GetKeyDown(KeyCode.J) && !invisibilidadActiva)
                    {
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 0.5f);
                        invisibilidadActiva = true;
                        esVisible = false;
                        Invoke("hacerVisible", 5);
                        Debug.Log("Invisible");
                    }
                    if (invisibilidadActiva)
                    {
                        inviDelay += Time.deltaTime;
                    }
                    if (inviDelay >= 60)
                    {
                        inviDelay = 0;
                        invisibilidadActiva = false;

                    }
                    break;
                case 2:
                    if (invisibilidad && Input.GetKeyDown(KeyCode.J) && !invisibilidadActiva)
                    {
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 0.5f);
                        invisibilidadActiva = true;
                        esVisible = false;
                        Invoke("hacerVisible", 5.5f);
                        Debug.Log("Invisible");
                    }
                    if (invisibilidadActiva)
                    {
                        inviDelay += Time.deltaTime;
                    }
                    if (inviDelay >= 57.5f)
                    {
                        inviDelay = 0;
                        invisibilidadActiva = false;

                    }
                    break;
                case 3:
                    if (invisibilidad && Input.GetKeyDown(KeyCode.J) && !invisibilidadActiva)
                    {
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 0.5f);
                        invisibilidadActiva = true;
                        esVisible = false;
                        Invoke("hacerVisible", 6);
                        Debug.Log("Invisible");
                    }
                    if (invisibilidadActiva)
                    {
                        inviDelay += Time.deltaTime;
                    }
                    if (inviDelay >= 55)
                    {
                        inviDelay = 0;
                        invisibilidadActiva = false;

                    }
                    break;
                case 4:
                    if (invisibilidad && Input.GetKeyDown(KeyCode.J) && !invisibilidadActiva)
                    {
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 0.5f);
                        invisibilidadActiva = true;
                        esVisible = false;
                        Invoke("hacerVisible", 6.5f);
                        Debug.Log("Invisible");
                    }
                    if (invisibilidadActiva)
                    {
                        inviDelay += Time.deltaTime;
                    }
                    if (inviDelay >= 52.5f)
                    {
                        inviDelay = 0;
                        invisibilidadActiva = false;

                    }
                    break;
                case 5:
                    if (invisibilidad && Input.GetKeyDown(KeyCode.J) && !invisibilidadActiva)
                    {
                        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 0.5f);
                        invisibilidadActiva = true;
                        esVisible = false;
                        Invoke("hacerVisible", 7);
                        Debug.Log("Invisible");
                    }
                    if (invisibilidadActiva)
                    {
                        inviDelay += Time.deltaTime;
                    }
                    if (inviDelay >= 50)
                    {
                        inviDelay = 0;
                        invisibilidadActiva = false;

                    }
                    break;

            }
        }
        if(cactusLVL>=1)
        {
            switch(cactusLVL)
            {
                case 1:
                    if (cactusPoder && Input.GetKeyDown(KeyCode.K) && !cactusActivado)
                    {
                        Instantiate(cactusPrefab, transform.position, Quaternion.identity);
                        cactusActivado = true;
                    }
                    if (cactusActivado)
                    {
                        cactusDelay += Time.deltaTime;
                    }
                    if (cactusDelay >= 17.5)
                    {
                        cactusDelay = 0;
                        cactusActivado = false;
                    }
                    break;
                case 2:
                    if (cactusPoder && Input.GetKeyDown(KeyCode.K) && !cactusActivado)
                    {
                        Instantiate(cactusPrefab, transform.position, Quaternion.identity);
                        cactusActivado = true;
                    }
                    if (cactusActivado)
                    {
                        cactusDelay += Time.deltaTime;
                    }
                    if (cactusDelay >= 15)
                    {
                        cactusDelay = 0;
                        cactusActivado = false;
                    }
                    break;
                case 3:
                    if (cactusPoder && Input.GetKeyDown(KeyCode.K) && !cactusActivado)
                    {
                        Instantiate(cactusPrefab, transform.position, Quaternion.identity);
                        cactusActivado = true;
                    }
                    if (cactusActivado)
                    {
                        cactusDelay += Time.deltaTime;
                    }
                    if (cactusDelay >= 12.5)
                    {
                        cactusDelay = 0;
                        cactusActivado = false;
                    }
                    break;
                case 4:
                    if (cactusPoder && Input.GetKeyDown(KeyCode.K) && !cactusActivado)
                    {
                        Instantiate(cactusPrefab, transform.position, Quaternion.identity);
                        cactusActivado = true;
                    }
                    if (cactusActivado)
                    {
                        cactusDelay += Time.deltaTime;
                    }
                    if (cactusDelay >= 10)
                    {
                        cactusDelay = 0;
                        cactusActivado = false;
                    }
                    break;
                case 5:
                    if (cactusPoder && Input.GetKeyDown(KeyCode.K) && !cactusActivado)
                    {
                        Instantiate(cactusPrefab, transform.position, Quaternion.identity);
                        cactusActivado = true;
                    }
                    if (cactusActivado)
                    {
                        cactusDelay += Time.deltaTime;
                    }
                    if (cactusDelay >= 7.5)
                    {
                        cactusDelay = 0;
                        cactusActivado = false;
                    }
                    break;
            }
        }
       if(dagaLVL>=1)
        {
            switch (dagaLVL)
            {
                case 1:
                    if (dagaPoder)
                    {
                        dagaDelay += Time.deltaTime;
                    }
                    if (dagaDelay >= 3)
                    {
                        dagaDelay = 0;
                        Instantiate(dagaPrefab, transform.position, transform.rotation * Quaternion.identity);
                    }
                    break;
                case 2:
                    if (dagaPoder)
                    {
                        dagaDelay += Time.deltaTime;
                    }
                    if (dagaDelay >= 2.75f)
                    {
                        dagaDelay = 0;
                        Instantiate(dagaPrefab, transform.position, transform.rotation * Quaternion.identity);
                    }
                    break;
                case 3:
                    if (dagaPoder)
                    {
                        dagaDelay += Time.deltaTime;
                    }
                    if (dagaDelay >= 2.5f)
                    {
                        dagaDelay = 0;
                        Instantiate(dagaPrefab, transform.position, transform.rotation * Quaternion.identity);
                    }
                    break;
                case 4:
                    if (dagaPoder)
                    {
                        dagaDelay += Time.deltaTime;
                    }
                    if (dagaDelay >= 2.25f)
                    {
                        dagaDelay = 0;
                        Instantiate(dagaPrefab, transform.position, transform.rotation * Quaternion.identity);
                    }
                    break;
                case 5:
                    if (dagaPoder)
                    {
                        dagaDelay += Time.deltaTime;
                    }
                    if (dagaDelay >= 2)
                    {
                        dagaDelay = 0;
                        Instantiate(dagaPrefab, transform.position, transform.rotation * Quaternion.identity);
                    }
                    break;          
            }
        }
        if (bombaLVL >= 1)
        {
            switch (bombaLVL)
            {
                case 1:
                    if (bombaPoder && Input.GetKeyDown(KeyCode.L) && !bombaActivada)
                    {
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                        bombaActivada = true;
                    }
                    if (bombaActivada)
                    {
                        bombaDelay += Time.deltaTime;
                    }
                    if (bombaDelay >= 20)
                    {
                        bombaDelay = 0;
                        bombaActivada = false;
                    }
                    break;
                case 2:
                    if (bombaPoder && Input.GetKeyDown(KeyCode.L) && !bombaActivada)
                    {
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                        bombaActivada = true;
                    }
                    if (bombaActivada)
                    {
                        bombaDelay += Time.deltaTime;
                    }
                    if (bombaDelay >= 18)
                    {
                        bombaDelay = 0;
                        bombaActivada = false;
                    }
                    break;
                case 3:
                    if (bombaPoder && Input.GetKeyDown(KeyCode.L) && !bombaActivada)
                    {
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                        bombaActivada = true;
                    }
                    if (bombaActivada)
                    {
                        bombaDelay += Time.deltaTime;
                    }
                    if (bombaDelay >= 16)
                    {
                        bombaDelay = 0;
                        bombaActivada = false;
                    }
                    break;
                case 4:
                    if (bombaPoder && Input.GetKeyDown(KeyCode.L) && !bombaActivada)
                    {
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                        bombaActivada = true;
                    }
                    if (bombaActivada)
                    {
                        bombaDelay += Time.deltaTime;
                    }
                    if (bombaDelay >= 14)
                    {
                        bombaDelay = 0;
                        bombaActivada = false;
                    }
                    break;
                case 5:
                    if (bombaPoder && Input.GetKeyDown(KeyCode.L) && !bombaActivada)
                    {
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                        bombaActivada = true;
                    }
                    if (bombaActivada)
                    {
                        bombaDelay += Time.deltaTime;
                    }
                    if (bombaDelay >= 12)
                    {
                        bombaDelay = 0;
                        bombaActivada = false;
                    }
                    break;
            }
        }

    }
    void hacerVisible()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, 1f);
        esVisible = true;
    }
    public void atacar(Vector2 pos)
    {
        Vector3 newdir = pos;
        newdir.z = 0f;

        newdir.x = pos.x - slash.transform.position.x;
        newdir.y = pos.y - slash.transform.position.y;

        float angle = Mathf.Atan2(newdir.y, newdir.x) * Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (SceneManager.GetActiveScene().name == "jugador2")
        {
            Vector3 targ = pos;
            targ.z = 0f;

            Vector3 objectPos = slash.transform.GetChild(0).position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            GameObject obj = Instantiate(flecha, slash.transform.GetChild(0).position, Quaternion.identity);
            obj.transform.localScale = new Vector3(tamanoAtaque, tamanoAtaque,1);
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            obj.GetComponent<flecha>().destino = targ.normalized;
        }
        if (SceneManager.GetActiveScene().name == "jugador1")
        {
            
            slash.transform.GetChild(0).GetComponent<Animator>().SetTrigger("atacar");
        }
        
    }
    private void FixedUpdate()
    {
        rd.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y * 100);
        if (vivo)
        {
            float movVertical = Input.GetAxisRaw("Vertical");
            float movHorizontal = Input.GetAxisRaw("Horizontal");
            Vector2 movimiento = new Vector2(movHorizontal, movVertical).normalized;
            rb.MovePosition((Vector2)transform.position + (movimiento * VelocidadBase * Time.fixedDeltaTime));
            timerAtaque += Time.fixedDeltaTime;
            //transform.Translate(movHorizontal, movVertical, 0);
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 1);
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 0);
            }
            Vector2 pos = Input.mousePosition;
            //Debug.Log(Input.mousePosition);
            pos.x = pos.x - (Screen.width / 2f);
            pos.y = pos.y - (Screen.height / 2f);
            Vector2 destinoAtaq = new Vector2(transform.position.x + (pos.x), transform.position.y + (pos.y));
            destinoAtaque = destinoAtaq;
            if (timerAtaque > velocidadAtaque)
            {
                atacar(destinoAtaq);
                timerAtaque = 0;
            }            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy" && vulnerable)
        {
            if (!evadirAtaque || Random.value >= probEvadir)
            {
                if (!escudoInmortal || vidaActual > collision.GetComponent<enemyController>().dano || activadoInmortal || escudoInmLVL<=0)
                {
                    if (!escudoProtector || escudoProtectorVida <= 0 || escudoProtLVL<=0)
                    {
                        vidaActual -= collision.GetComponent<enemyController>().dano;
                        if (armaduraEspinas && armEspLVL>=1)
                        {
                            switch(armEspLVL)
                            {
                                case 1:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 4;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 4)*100)/100f + "";
                                    break;
                                case 2:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 3.5f;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj2 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 3.5f) * 100) / 100f + "";
                                    break;
                                case 3:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 2;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj3 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 2) * 100) / 100f + "";
                                    break;
                                case 4:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 1.5f;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj4 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 1.5f) * 100) / 100f + "";
                                    break;
                                case 5:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj5 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (DanoBase) + "";
                                    break;

                            }

                        }
                        HacerInvulnerable();
                        recibeDano();
                    }
                    else if (escudoProtector && escudoProtectorVida > 0 && escudoProtLVL>=1)
                    {
                        escudoProtectorVida -= collision.GetComponent<enemyController>().dano;
                        if (armaduraEspinas && armEspLVL >= 1)
                        {
                            switch (armEspLVL)
                            {
                                case 1:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 4;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 4) * 100) / 100f + "";
                                    break;
                                case 2:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 3.5f;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj2 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 3.5f) * 100) / 100f + "";
                                    break;
                                case 3:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 2;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj3 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 2) * 100) / 100f + "";
                                    break;
                                case 4:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase / 1.5f;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj4 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.Round((DanoBase / 1.5f) * 100) / 100f + "";
                                    break;
                                case 5:
                                    collision.gameObject.GetComponent<enemyController>().vida -= DanoBase;
                                    collision.gameObject.GetComponent<enemyController>().efectoFlash();
                                    GameObject obj5 = Instantiate(hmThorns, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z), Quaternion.identity);
                                    obj5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (DanoBase) + "";
                                    break;

                            }

                        }
                        HacerInvulnerable();

                    }
                }
                else if (escudoInmortal && collision.GetComponent<enemyController>().dano >= vidaActual && !activadoInmortal && escudoInmLVL>=1)
                {
                    activadoInmortal = true;
                    vidaActual = 1;
                    HacerInvulnerableEscudo();
                }
            }
            else
            {
                HacerInvulnerable();
            }
        }
    }
    void HacerInvulnerable()
    {
        vulnerable = false;
        Invoke("HacerVulnerable", 2.0f);
    }
    void HacerVulnerable()
    {
        vulnerable = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }
    void HacerInvulnerableEscudo()
    {
        vulnerable = false;
        switch (escudoInmLVL)
        {
            case 1:
                Invoke("HacerVulnerable", 5.0f);
                break;
            case 2:
                Invoke("HacerVulnerable", 5.5f);
                break;
            case 3:
                Invoke("HacerVulnerable", 6.0f);
                break;
            case 4:
                Invoke("HacerVulnerable", 6.5f);
                break;
            case 5:
                Invoke("HacerVulnerable", 7.0f);
                break;
        }
    }
    public void getExp(float expGained)
    {
        exp += expGained;
    }
    public void curar(float curacion)
    {
        vidaActual += curacion*porcentajeCuraciones;
        vidaActual = vidaActual > vidaMax ? vidaMax : vidaActual;
        GameObject obj = Instantiate(hmHeal, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curacion + "";
    }
    void recibeDano()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("quitarDano", 0.3f);
    }
    void quitarDano()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = origMaterial;

    }
    void desplegarMenuMuerte()
    {
        menuMuerte.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ((Mathf.Round(wm.timeTotal*100))/100)+"s";
        menuMuerte.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = menuMuerte.transform.parent.GetComponent<interfazInGameManager>().puntos + "";
        menuMuerte.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = (int)Mathf.Round(menuMuerte.transform.parent.GetComponent<interfazInGameManager>().puntos / 5f) + "$";
        gm.dinero += (int)Mathf.Round(menuMuerte.transform.parent.GetComponent<interfazInGameManager>().puntos / 5f);
        menuMuerte.SetActive(true);
        Time.timeScale = 0;
    }
}
