using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eliminarEnemigos : MonoBehaviour
{
    private void Start()
    {
        Invoke("destruir", 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Enemy")){
            other.GetComponent<enemyController>().vida = -1;
        }
    }
    private void destruir()
    {
        Destroy(gameObject);
    }
}
