using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class llamarAds : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        if((SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2") && !gameManager.instance.premium)
        {
            LogicaAds.instance.MostrarBanner();
        }
    }
    public void MostrarBanner()
    {
        if(!gameManager.instance.premium)
        {
            LogicaAds.instance.MostrarBanner();

        }
    }
    public void MostrarInters()
    {
        if(!gameManager.instance.premium)
        {
            LogicaAds.instance.MostrarInters();

        }


    }
    public void MostrarReward()
    {
        if(!gameManager.instance.premium)
        {
            LogicaAds.instance.MostrarReward();

        }
        else
        {
            GameObject.Find("Heroe").GetComponent<PlayerController>().revivir();
        }



    }
    
}
