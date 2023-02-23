using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proy2 : MonoBehaviour
{
    public void spawnearRayo()
    {
        GetComponent<Animator>().SetTrigger("rayo");
    }
    public void destruir()
    {
        Destroy(gameObject);
    }
}
