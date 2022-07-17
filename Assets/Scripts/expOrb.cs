using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expOrb : MonoBehaviour
{
    public float exp;
    public Collider2D collis;
    public bool moverse=false;
    public GameObject player;
    public float rangoRecojo;

    private void Start()
    {
        player = GameObject.Find("Heroe");
    }
    private void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < rangoRecojo)
        {
            moverse = true;
        }
    }
    private void FixedUpdate()
    {
        if (moverse)
        {
            float x = player.transform.position.x - transform.position.x;
            float y = player.transform.position.y - transform.position.y;
            Vector2 dir = new Vector2(x, y).normalized;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + dir.x * Time.deltaTime * 13, transform.position.y + dir.y * Time.deltaTime * 13));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Heroe")
        {
            if(transform.tag == "Exp")
            {
                collision.GetComponent<PlayerController>().getExp(exp);
                Destroy(gameObject);
            }
            if(transform.tag == "Corazon")
            {
                collision.GetComponent<PlayerController>().curar(exp);
                Destroy(gameObject);
            }
        }
    }
}
