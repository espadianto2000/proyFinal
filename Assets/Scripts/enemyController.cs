using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Material origMaterial;
    public Material flashMaterial;
    public GameObject player;
    int estado = 1;
    public bool auxVolteado;
    bool derecha = true;
    public Rigidbody2D rb;
    public Renderer rd;
    public WaveManager waveM;
    public GameObject corazon;
    [Header("stats")]
    public float velocidad;
    public float vidaMax;
    public float vida;
    public float distanciaX;
    public float distanciaY;
    // Start is called before the first frame update
    void Start()
    {
        origMaterial = transform.GetChild(0).GetComponent<SpriteRenderer>().material;
        player = GameObject.Find("Heroe");
        vida = vidaMax;
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponentInChildren<Renderer>();
        waveM = GameObject.Find("waveManager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vida<=0)
        {
            Instantiate(waveM.exps[(waveM.nivelDificultad)<waveM.exps.Length?waveM.nivelDificultad:waveM.exps.Length-1], transform.position,Quaternion.identity);
            if(Random.Range(0,100) < 5)
            {
                Instantiate(waveM.corazon, transform.position, Quaternion.identity);
            }
            Destroy(transform.gameObject);
        }
        //Debug.Log(transform.position.x);
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
                        rb.MovePosition((Vector2)transform.position + (new Vector2(dirx, diry).normalized) * velocidad * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.deltaTime);
                        break;
                    case 2:
                        break;
                }
            }
            else
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
                        rb.MovePosition((Vector2)transform.position + (new Vector2(dirx, diry).normalized) * velocidad * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.deltaTime);
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
    }
    private void FixedUpdate()
    {
        rd.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y*100);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "slash")
        {
            efectoFlash();
        }
    }
    void efectoFlash()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("quitarFlash", 0.3f);
    }
    void quitarFlash()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = origMaterial;

    }
}
