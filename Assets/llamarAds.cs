using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class llamarAds : MonoBehaviour
{
    public GameObject dummyBanner;
    public GameObject dummyInters;
    public GameObject dummyReward;
    // Start is called before the first frame update
    private void Start()
    {
        if((SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2") && !gameManager.instance.premium)
        {
            StartCoroutine(checkInternetConnection1());
        }
    }
    public void MostrarBanner()
    {
        if(!gameManager.instance.premium)
        {
            StartCoroutine(checkInternetConnection1());
        }
    }
    public void MostrarInters()
    {
        if(!gameManager.instance.premium)
        {
            StartCoroutine(checkInternetConnection2());

        }


    }
    public void MostrarReward()
    {
        StartCoroutine(checkInternetConnection3());


        

    }
    IEnumerator checkInternetConnection1()
    {
        WWW www = new WWW("http://google.com/");
   
        yield return www;
        if (www.error != null)
        {
            //error, mostrar dummy
            Instantiate(dummyBanner);


        }
        else
        {
            LogicaAds.instance.MostrarBanner();
        }
    }
    IEnumerator checkInternetConnection2()
    {
        WWW www = new WWW("http://google.com/");

        yield return www;
        if (www.error != null)
        {
            //error, mostrar dummy
            Instantiate(dummyInters);
        }
        else
        {
            LogicaAds.instance.MostrarInters();
        }

    }
    IEnumerator checkInternetConnection3()
    {
        WWW www = new WWW("http://google.com/");

        yield return www;
        if (www.error != null)
        {
            //error, mostrar dummy
            Instantiate(dummyReward);

        }
        else
        {
            //do somtehing
            LogicaAds.instance.MostrarReward();
        }
    }
}
