using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flecha : MonoBehaviour
{
    public Vector2 destino;
    void Start()
    {
        Invoke("destruir", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(destino != null)
        {
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + destino.x * Time.fixedDeltaTime * 10, transform.position.y + destino.y * Time.fixedDeltaTime * 10));
        }
    }
    public void destruir()
    {
        Destroy(gameObject);
    }
}
