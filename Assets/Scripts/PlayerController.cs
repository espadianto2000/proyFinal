using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool vulnerable = true;
    [Header("Stats")]
    public float VelocidadBase;
    public float DanoBase;
    public float vidaMax;
    public float vidaActual;
    public bool vivo;
    public float tamanoAtaque;
    public float velocidadAtaque;

    [Header("movimiento")]
    public Vector2 destinoAtaque = new Vector2(0,0);
    public Renderer rd;

    [Header("ataque")]
    public GameObject slash;
    public float timerAtaque=0;
    void Start()
    {
        vivo = true;
        rd = GetComponentInChildren<Renderer>();
        vidaActual = vidaMax;
    }
    void Update()
    {
        if (vivo)
        {
            float movVertical = Input.GetAxis("Vertical") * VelocidadBase * Time.deltaTime;
            float movHorizontal = Input.GetAxis("Horizontal") * VelocidadBase * Time.deltaTime;
            timerAtaque += Time.deltaTime;
            transform.Translate(movHorizontal, movVertical, 0);
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
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
            Vector2 destinoAtaq = new Vector2(transform.position.x+(pos.x),slash.transform.position.y + (pos.y));
            destinoAtaque = destinoAtaq;
            if (timerAtaque > velocidadAtaque)
            {
                atacar(destinoAtaq);
                timerAtaque = 0;
            }


        }
        if(vidaActual <= 0)
        {
            vivo = false;
        }
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && vulnerable)
        {
            vidaActual -= 10;

            Invoke("HacerInvulnerable", 0f);


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
    }
}
