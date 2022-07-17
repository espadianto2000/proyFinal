using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Material origMaterial;
    public Material flashMaterial;
    public bool vulnerable = true;
    public Rigidbody2D rb;
    public gameManager gm;
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

    [Header("movimiento")]
    public Vector2 destinoAtaque = new Vector2(0,0);
    public Renderer rd;

    [Header("ataque")]
    public GameObject slash;
    public float timerAtaque=0;

    [Header("subidaNivel")]
    public float xpNecesaria=200;
    public float exp;
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
            porcentajeAparicionCorazones = porcentajeAparicionCorazones + (0.03f * gm.nivelSpawnVida);
            porcentajeCuraciones = porcentajeCuraciones + (0.04f * gm.nivelCuracionExtra);
            velocidadAtaque = velocidadAtaque * (1 - (0.05f * gm.nivelVelocidadAtaqueExtra));
            velocidadAtaque = velocidadAtaque < 0.3f ? 0.3f : velocidadAtaque;
        }
        origMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        vivo = true;
        rd = GetComponentInChildren<Renderer>();
        vidaActual = vidaMax;
    }
    void Update()
    {
        if(!vulnerable)
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
        if(vidaActual <= 0)
        {
            vivo = false;
        }
        if (exp >= xpNecesaria)
        {
            exp = exp - xpNecesaria;
            Debug.Log("se sube de nivel");
            //manejar subida de nivel
            xpNecesaria = xpNecesaria * 1.5f;
        }
        vidaSlider.value = vidaActual / vidaMax;
    }
    
    public void atacar(Vector2 pos)
    {
        Vector3 newdir = pos;
        newdir.z = 0f;

        newdir.x = pos.x - slash.transform.position.x;
        newdir.y = pos.y - slash.transform.position.y;

        float angle = Mathf.Atan2(newdir.y,newdir.x)*Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        slash.transform.GetChild(0).GetComponent<Animator>().SetTrigger("atacar");
    }
    private void FixedUpdate()
    {
        rd.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y * 100);
        if (vivo)
        {
            float movVertical = Input.GetAxis("Vertical");
            float movHorizontal = Input.GetAxis("Horizontal");
            Vector2 movimiento = new Vector2(movHorizontal, movVertical).normalized;
            rb.MovePosition((Vector2)transform.position + (movimiento * VelocidadBase * Time.fixedDeltaTime));
            timerAtaque += Time.fixedDeltaTime;
            //transform.Translate(movHorizontal, movVertical, 0);
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
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
            Vector2 destinoAtaq = new Vector2(transform.position.x + (pos.x), slash.transform.position.y + (pos.y));
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
        if(collision.tag == "Enemy" && vulnerable)
        {
            vidaActual -= 10;

            HacerInvulnerable();
            recibeDano();
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
    public void getExp(float expGained)
    {
        exp += expGained;
    }
    public void curar(float curacion)
    {
        vidaActual += curacion*porcentajeCuraciones;
        vidaActual = vidaActual > vidaMax ? vidaMax : vidaActual;
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
}
