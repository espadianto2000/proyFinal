using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proy2mini : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heroe"))
        {
            collision.GetComponent<PlayerController>().recibirDanoJugadorPublico(15);
        }
    }
}
