using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dumAd : MonoBehaviour
{
    public GameObject ad;
    public Text textoTiempo;
    public GameObject botonCerrar;
    public float time = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textoTiempo.text = (Mathf.FloorToInt(time % 60)).ToString() + " second(s) remaining";
        if(time>0)
        {
            time -= Time.unscaledDeltaTime;
        }
        else
        {
            botonCerrar.gameObject.SetActive(true);
            textoTiempo.gameObject.SetActive(false);
        }
    }
    public void destruir()
    {
        Destroy(ad);
        Time.timeScale = 1;
        GameObject.Find("Heroe").GetComponent<PlayerController>().revivir();
        GameObject.Find("Heroe").GetComponent<PlayerController>().fueraPausa = false;
    }
}
