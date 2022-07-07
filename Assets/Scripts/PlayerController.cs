using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float VelocidadBase;
    public float DanoBase;
    public float vidaMax;
    public float vidaActual;
    public bool vivo;

    [Header("movimiento")]
    public Vector2 destinoAtaque = new Vector2(0,0);
    void Start()
    {
        vivo = true;
    }
    void Update()
    {
        if (vivo)
        {
            float movVertical = Input.GetAxis("Vertical") * VelocidadBase * Time.deltaTime;
            float movHorizontal = Input.GetAxis("Horizontal") * VelocidadBase * Time.deltaTime;
            transform.Translate(movHorizontal, movVertical, 0);
            if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }else if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            Vector2 pos = Input.mousePosition;
            //Debug.Log(Input.mousePosition);
            pos.x = pos.x - (Screen.width / 2f);
            pos.y = pos.y - (Screen.height / 2f);
            Vector2 destinoAtaq = new Vector2(transform.position.x+(pos.x*1000),transform.position.y + (pos.y*1000));
            destinoAtaque = destinoAtaq;
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector2)transform.position, destinoAtaque);
    }
}
