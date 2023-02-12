using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToShow : MonoBehaviour
{
    [SerializeField]
    public GameObject cerrar;
    public float tiempoCerrar = 5f;
    public GameObject GODummy;

    void Start()
    {
        //Invoke("cerrarItem", 4);
        Time.timeScale = 0;
    }

    private void cerrarItem()
    {
        cerrar.SetActive(true);
    }
    public void dest()
    {
        Time.timeScale = 1;
        GameObject.Find("Heroe").GetComponent<PlayerController>().fueraPausa = false;
        Destroy(GODummy);

    }
     void Update()
    {
        if(tiempoCerrar > 0)
        {
            tiempoCerrar -= Time.unscaledDeltaTime;
        }
        else
        {
            cerrar.SetActive(true);
        }

    }
}
