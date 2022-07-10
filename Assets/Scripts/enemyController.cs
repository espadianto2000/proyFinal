using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    int estado = 1;
    public bool auxVolteado;
    bool derecha = true;
    [Header("stats")]
    public float velocidad = 9;
    public float vidaMax;
    public float vida;
    public float distanciaX;
    public float distanciaY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Heroe");
        vida = vidaMax;

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.x);
        if (Mathf.Abs(transform.position.x - player.transform.GetChild(0).transform.position.x) >= distanciaX || Mathf.Abs(transform.position.y - player.transform.GetChild(0).transform.position.y) >= distanciaY)
        {


            {


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
                            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.deltaTime);
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
                            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<PlayerController>().slash.transform.position, velocidad * Time.deltaTime);
                            break;
                        case 2:
                            break;
                    }
                }
            }
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetInteger("estado", 0);
        }
    }
}
