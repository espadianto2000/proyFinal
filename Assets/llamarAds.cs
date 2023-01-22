using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llamarAds : MonoBehaviour
{
    // Start is called before the first frame update

    public void MostrarBanner()
    {
        LogicaAds.instance.MostrarBanner();
    }
    public void MostrarInters()
    {
        LogicaAds.instance.MostrarInters();


    }
    public void MostrarReward()
    {
        LogicaAds.instance.MostrarReward();

    }
}
