using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proy3 : MonoBehaviour
{
    public float tiempoCambio = 3f;
    public GameObject player;
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Random.Range(0f,359f)));
        player = GameObject.Find("Heroe");
        Destroy(gameObject,10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,transform.position+(transform.right * 200f),Time.fixedDeltaTime * 7.5f);
        if (tiempoCambio <= 0 && tiempoCambio > -20)
        {
            tiempoCambio = -21;
            //What is the difference in position?
            Vector3 diff = (player.transform.position - transform.position);

            //We use aTan2 since it handles negative numbers and division by zero errors. 
            float angle = Mathf.Atan2(diff.y, diff.x);

            //Now we set our new rotation. 
            transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
        }
        else if(tiempoCambio > 0)
        {
            tiempoCambio -= Time.fixedDeltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heroe"))
        {
            collision.GetComponent<PlayerController>().recibirDanoJugadorPublico(10);
        }
    }
}
