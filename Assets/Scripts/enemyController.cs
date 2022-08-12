using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public AudioSource sonidoMuerte;
    private Material origMaterial;
    public Material flashMaterial;
    public GameObject player;
    int estado = 1;
    public bool auxVolteado;
    bool derecha = true;
    public Rigidbody2D rb;
    public Renderer rd;
    public WaveManager waveM;
    //public GameObject corazon;
    public GameObject hm;
    public GameObject hmCrit;
    public GameObject interfazManager;
    [Header("stats")]
    public float velocidad;
    public float vidaMax;
    public float vida;
    public float distanciaX;
    public float distanciaY;
    public float dano;
    // Start is called before the first frame update
    void Start()
    {
        origMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        player = GameObject.Find("Heroe");
        vida = vidaMax;
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponentInChildren<Renderer>();
        waveM = GameObject.Find("waveManager").GetComponent<WaveManager>();
        interfazManager = GameObject.Find("interfazJuego");
        sonidoMuerte = GameObject.Find("AudioMuerteEnemigo").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(vida<=0)
        {
            GameObject obj = Instantiate(waveM.exps[(waveM.nivelDificultad)<waveM.exps.Length?waveM.nivelDificultad:waveM.exps.Length-1], transform.position,Quaternion.identity);
            obj.GetComponent<expOrb>().exp += 1000 * waveM.iteracionOleada;
            obj.transform.localScale = new Vector3(obj.transform.localScale.x * (1 + (1 * waveM.iteracionOleada)), obj.transform.localScale.y * (1 + (1 * waveM.iteracionOleada)), 1);
            if (Random.Range(0,100) < player.GetComponent<PlayerController>().porcentajeAparicionCorazones*100)
            {
                Instantiate(waveM.corazon, transform.position, Quaternion.identity);
                if(GameObject.FindGameObjectsWithTag("Exp").Length > 75)
                {
                    if(Random.Range(0,100) < 10)
                    {
                        Instantiate(waveM.iman);
                    }
                }
            }
            interfazManager.GetComponent<interfazInGameManager>().puntos += 1*player.GetComponent<PlayerController>().porcentajePuntos;
            sonidoMuerte.Play();
            Destroy(transform.gameObject);
        }
        //Debug.Log(transform.position.x);
        
    }
    private void FixedUpdate()
    {
        rd.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y*100);
        if (Mathf.Abs(transform.position.x - player.transform.GetChild(0).transform.position.x) >= distanciaX || Mathf.Abs(transform.position.y - player.transform.GetChild(0).transform.position.y) >= distanciaY)
        {
            float dirx = player.transform.position.x - transform.position.x;
            float diry = player.transform.position.y - transform.position.y;
            if (auxVolteado)
            {
                if (transform.GetChild(0).GetComponent<Animator>().GetInteger("estado") != estado)
                {
                    transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", estado);
                }
                if (transform.position.x < player.transform.GetChild(0).transform.position.x && derecha)
                {
                    derecha = false;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
                }
                else if (transform.position.x > player.transform.GetChild(0).transform.position.x && !derecha)
                {
                    derecha = true;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
                }
                switch (estado)
                {
                    case 0:
                        break;
                    case 1:
                        rb.MovePosition((Vector2)transform.position + (new Vector2(dirx, diry).normalized) * velocidad * Time.fixedDeltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.fixedDeltaTime);
                        break;
                    case 2:
                        break;
                }
            }
            else if(player.GetComponent<PlayerController>().esVisible)
            {
                if (transform.GetChild(0).GetComponent<Animator>().GetInteger("estado") != estado)
                {
                    transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", estado);
                }
                if (transform.position.x > player.transform.GetChild(0).transform.position.x && derecha)
                {
                    derecha = false;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
                }
                else if (transform.position.x < player.transform.GetChild(0).transform.position.x && !derecha)
                {
                    derecha = true;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
                }
                switch (estado)
                {
                    case 0:
                        break;
                    case 1:
                        rb.MovePosition((Vector2)transform.position + (new Vector2(dirx, diry).normalized) * velocidad * Time.fixedDeltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.fixedDeltaTime);
                        break;
                    case 2:
                        break;
                }
            }
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 0);
            rb.velocity = Vector2.zero;
        }
        if(!player.GetComponent<PlayerController>().esVisible)
        {
            transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     /*   if(collision.tag == "slash")
        {
            efectoFlash();
            GameObject obj = Instantiate(hm, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (player.GetComponent<PlayerController>().DanoBase * player.GetComponent<PlayerController>().multiplicadorDanoUlti) + "";
        }*/
        if(collision.tag == "cactus")
        {
            collision.GetComponent<cactus>().vida -= 10;
            efectoFlash();
            vida -= 10;

        }
    }
    public void efectoFlash()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("quitarFlash", 0.3f);
    }
    void quitarFlash()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = origMaterial;

    }
}
