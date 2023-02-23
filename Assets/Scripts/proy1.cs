using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proy1 : MonoBehaviour
{
    bool perseguir = true;
    GameObject player = null;


    private void Start()
    {
        player = GameObject.Find("Heroe");
    }
    void Update()
    {
        if (perseguir)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 20 * Time.deltaTime);
        }
    }
    public void ActivarDano()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    public void terminarPersecucion()
    {
        perseguir = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heroe"))
        {
            collision.GetComponent<PlayerController>().recibirDanoJugadorPublico(20);
        }
    }
    public void destruir()
    {
        Destroy(gameObject, 0.2f);
    }
}
