using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    GameObject player;
    int estado = 1;
    public bool auxVolteado;
    bool derecha = true;
    [Header("stats")]
    public float velocidad = 9;
    public float vidaMax;
    public float vida;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        vida = vidaMax;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)>=-1f)
        {


            if (auxVolteado)
            {
                if (GetComponent<Animator>().GetInteger("estado") != estado)
                {
                    GetComponent<Animator>().SetInteger("estado", estado);
                }
                if (transform.position.x < player.transform.position.x && derecha)
                {
                    derecha = false;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                else if (transform.position.x > player.transform.position.x && !derecha)
                {
                    derecha = true;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
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


                if (GetComponent<Animator>().GetInteger("estado") != estado)
                {
                    GetComponent<Animator>().SetInteger("estado", estado);
                }
                if (transform.position.x > player.transform.position.x && derecha)
                {
                    derecha = false;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                }
                else if (transform.position.x < player.transform.position.x && !derecha)
                {
                    derecha = true;
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
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
        else
        {
            GetComponent<Animator>().SetInteger("estado", 0);
        }
    }
}
