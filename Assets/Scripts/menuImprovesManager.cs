using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class menuImprovesManager : MonoBehaviour
{
    public gameManager gm;
    public GameObject[] botones;
    public GameObject dinero;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        botones = new GameObject[] { transform.GetChild(1).gameObject, transform.GetChild(2).gameObject, transform.GetChild(3).gameObject, transform.GetChild(4).gameObject, transform.GetChild(5).gameObject, transform.GetChild(6).gameObject, transform.GetChild(7).gameObject, transform.GetChild(8).gameObject, transform.GetChild(9).gameObject, transform.GetChild(10).gameObject, transform.GetChild(11).gameObject, transform.GetChild(12).gameObject };
        actualizar();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void mejorar(int orden)
    {
        gm.mejorar(orden);
        actualizar();
    }
    public void desbloquear(int orden)
    {
        gm.desbloquear(orden);
        actualizar();
    }
    public void actualizar()
    {
        dinero.GetComponent<TextMeshProUGUI>().text = gm.dinero + "$";
        for(int i=0;i<botones.Length-2;i++)
        {
            //Debug.Log("boton: " + i);
            botones[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "nivel: " + gm.statsArr[i];
            if(gm.statsArr[i] != 0)
            {
                botones[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Costo: " + (100 + Mathf.Floor(100 * (gm.statsArr[i] * gm.statsArr[i]))) + "$";
            }
            else botones[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Costo: " +(100 + 0)+"$";
        }
        botones[botones.Length - 2].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gm.desbloquearPersonaje2? "desbloqueado" : "bloqueado";
        botones[botones.Length - 1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (gm.desbloquearUlti ? "desbloqueado" : "bloqueado");
        botones[botones.Length - 2].transform.GetChild(3).gameObject.SetActive(gm.desbloquearPersonaje2 ? false : true);
        botones[botones.Length - 1].transform.GetChild(3).gameObject.SetActive(gm.desbloquearUlti ? false : true);
        botones[botones.Length - 2].transform.GetChild(4).GetComponent<Button>().interactable = gm.desbloquearPersonaje2 ? false : true;
        botones[botones.Length - 1].transform.GetChild(4).GetComponent<Button>().interactable = gm.desbloquearUlti ? false : true;
        botones[botones.Length - 2].transform.GetChild(4).GetComponent<boton>().enabled = gm.desbloquearPersonaje2 ? false : true;
        botones[botones.Length - 1].transform.GetChild(4).GetComponent<boton>().enabled = gm.desbloquearUlti ? false : true;
        gm.guardar();
    }
}
