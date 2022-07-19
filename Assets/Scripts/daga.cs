using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daga : MonoBehaviour
{
    public GameObject player;
    private Vector3 direccion;
    private Vector3 orientacion;
    public float velocidad = 2.0f;
    public float rotationSpeed;
    public float aux;
    public bool rotado = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Heroe");
        direccion = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;
        //transform.rotation = Quaternion.LookRotation(direccion * velocidad, Vector3.back);
        orientacion = direccion * velocidad;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.GetComponent<SpriteRenderer>().color.r, transform.GetComponent<SpriteRenderer>().color.g, transform.GetComponent<SpriteRenderer>().color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = new Vector3(transform.position.x + (orientacion.x* Time.deltaTime), transform.position.y + (orientacion.y * Time.deltaTime), 0f);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, direccion.z);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direccion);
        aux += Time.deltaTime;
        if(aux<= 0.1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            rotado = false;
        }
        else if(!rotado)
        {

            rotar();

        }
        if(aux>=10)
        {
            Destroy(gameObject);
        }
    }
    void rotar()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 38);
        rotado = true;
        hacerVisible();

    }
    void hacerVisible()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.GetComponent<SpriteRenderer>().color.r, transform.GetComponent<SpriteRenderer>().color.g, transform.GetComponent<SpriteRenderer>().color.b, 1f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(player.GetComponent<PlayerController>().dagaLVL>=1)
            {
                switch(player.GetComponent<PlayerController>().dagaLVL)
                {
                    case 1:
                        collision.gameObject.GetComponent<enemyController>().vida -= player.GetComponent<PlayerController>().DanoBase / 3;
                        collision.gameObject.GetComponent<enemyController>().efectoFlash();
                        Destroy(gameObject);
                        break;
                    case 2:
                        collision.gameObject.GetComponent<enemyController>().vida -= player.GetComponent<PlayerController>().DanoBase / 2.75f;
                        collision.gameObject.GetComponent<enemyController>().efectoFlash();
                        Destroy(gameObject);
                        break;
                    case 3:
                        collision.gameObject.GetComponent<enemyController>().vida -= player.GetComponent<PlayerController>().DanoBase / 2.5f;
                        collision.gameObject.GetComponent<enemyController>().efectoFlash();
                        Destroy(gameObject);
                        break;
                    case 4:
                        collision.gameObject.GetComponent<enemyController>().vida -= player.GetComponent<PlayerController>().DanoBase / 2;
                        collision.gameObject.GetComponent<enemyController>().efectoFlash();
                        Destroy(gameObject);
                        break;
                    case 5:
                        collision.gameObject.GetComponent<enemyController>().vida -= player.GetComponent<PlayerController>().DanoBase / 1.75f;
                        collision.gameObject.GetComponent<enemyController>().efectoFlash();
                        Destroy(gameObject);
                        break;

                }
            }
            
        }
    }
}
    