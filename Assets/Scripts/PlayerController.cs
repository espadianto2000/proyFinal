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
            Debug.Log(Input.mousePosition);
        }
    }
}
