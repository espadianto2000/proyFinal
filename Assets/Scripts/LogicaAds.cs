using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaAds : MonoBehaviour
{
    public GameObject dummyBanner;
    public GameObject dummyInters;
    public GameObject dummyReward;

    public static LogicaAds instance;
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    public GameObject cargaPrefab;
    public GameObject cargaGO;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        PedirInterstitial();
        //PedirReward();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Awake()
    {
        if(LogicaAds.instance == null)
        {
            LogicaAds.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void PedirBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9842563229947557/8112047865";//anuncio banner real
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerAd = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create a 320x50 banner at the top of the screen.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerAd.LoadAd(request);
    }
    public void cerrarBanner()
    {
        bannerAd.Destroy();
    }
    private void PedirInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9842563229947557/2452527079";//anuncio inters real
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitialAd = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
       // if(SystemInfo.deviceType == DeviceType.Handheld)
        //{
         //   StartCoroutine(DelayPausa());

        //}
        Debug.Log("se cerro el ad");

    }
    IEnumerator DelayPausa()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("Heroe").GetComponent<PlayerController>().fueraPausa = true;
        Time.timeScale = 0;

    }

    public void MostrarBanner()
    {
        Debug.Log("Hola?");
        StartCoroutine(checkInternetConnection1());
    }
    public void MostrarInters()
    {
        Debug.Log("Inters");
        StartCoroutine(checkInternetConnection2());

        
        // interstitialAd.Destroy();

    }
    public void MostrarReward()
     {
        
            
            StartCoroutine(checkInternetConnection3());

        
        //rewardedAd.Show();

    }

     
    public void PedirReward()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9842563229947557/6607394505";//anuncio bonoficado real
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        Debug.Log("Se instancio el prefab");

    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        if(!gameManager.instance.premium)
        {
            rewardedAd.Show();

        }
        else
        {
            if (SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2")
            {
                GameObject.Find("Heroe").GetComponent<PlayerController>().revivir();

            }
        }
        //StartCoroutine(Esperar())


    }


    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Destroy(GameObject.Find("Carga(Clone)"));
        GameObject.Find("conexion").GetComponent<Image>().color = Color.red;


    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Destroy(GameObject.Find("Carga(Clone)"));

        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        if(SceneManager.GetActiveScene().name == "jugador1" || SceneManager.GetActiveScene().name == "jugador2")
        {
            GameObject.Find("Heroe").GetComponent<PlayerController>().revivir();

        }
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //HACER UN BOOLEANDO Y PASARLO AL ADCLOSED, SI NO HAS VISTO EL VIDEO ES FALSE Y NO SE TE REVIVE
        Debug.Log("Recomensa reclamada");
        //GameObject.Find("visto").GetComponent<Text>().text = "Visto";
    }
    IEnumerator checkInternetConnection1()
    {
        WWW www = new WWW("http://google.com/");

        yield return www;
        if (www.error!=null)
        {
            //error, mostrar dummy
            Instantiate(dummyBanner);


        }
        else
        {
            PedirBanner();
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
            interstitialAd.Show();


            PedirInterstitial();
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
        if (!gameManager.instance.premium)
        {
            Instantiate(cargaPrefab);

        }
        PedirReward();

        }
    }
}

