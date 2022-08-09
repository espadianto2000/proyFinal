using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class interfazInGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float puntos = 0;
    public gameManager gm;
    public TextMeshProUGUI displayPuntos;
    public GameObject ulti;
    public GameObject player;

    [Header("itemsIcon")]
    public GameObject espinas;
    public GameObject roboVida;
    public GameObject escudo;
    public GameObject inmortalidad;
    public GameObject invisibilidad;
    public GameObject cuchillo;
    public GameObject evasion;
    public GameObject cactus;
    public GameObject bomba;

    [Header("valoresStats")]
    public GameObject vidaMax;
    public GameObject atk;
    public GameObject velocidad;
    public GameObject critico;
    public GameObject velocAtk;
    public GameObject Alcance;
    public GameObject XPMultiplier;
    public GameObject PtsMultiplier;
    public GameObject DineroMultiplier;

    [Header("joystick")]
    [SerializeField] private GameObject joystick;
    void Start()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            joystick.SetActive(true);
        }
        if (GameObject.Find("gameManager"))
        {
            gm = GameObject.Find("gameManager").GetComponent<gameManager>();
            if (gm.desbloquearUlti)
            {
                ulti.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        displayPuntos.text =  (Mathf.Round(puntos*100))/100 + "";
        ulti.transform.GetChild(0).GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().cargaUlti;
        ulti.transform.GetChild(1).GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().cargaUlti;
        ulti.transform.GetChild(3).GetComponent<Image>().fillAmount = player.GetComponent<PlayerController>().cargaUlti;
        if (player.GetComponent<PlayerController>().cargaUlti >= 1)
        {
            ulti.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (!espinas.active && player.GetComponent<PlayerController>().armaduraEspinas)
        {
            espinas.SetActive(true);
        }
        if (!roboVida.active && player.GetComponent<PlayerController>().roboVida)
        {
            roboVida.SetActive(true);
        }
        if (!escudo.active && player.GetComponent<PlayerController>().escudoProtector)
        {
            escudo.SetActive(true);
        }
        if (!inmortalidad.active && player.GetComponent<PlayerController>().escudoInmortal)
        {
            inmortalidad.SetActive(true);
        }
        if (!invisibilidad.active && player.GetComponent<PlayerController>().invisibilidad)
        {
            invisibilidad.SetActive(true);
        }
        if (!cuchillo.active && player.GetComponent<PlayerController>().dagaPoder)
        {
            cuchillo.SetActive(true);
        }
        if (!evasion.active && player.GetComponent<PlayerController>().evadirAtaque)
        {
            evasion.SetActive(true);
        }
        if (!cactus.active && player.GetComponent<PlayerController>().cactusPoder)
        {
            cactus.SetActive(true);
        }
        if (!bomba.active && player.GetComponent<PlayerController>().bombaPoder)
        {
            bomba.SetActive(true);
        }
    }
    public void reiniciar()
    {
        gm.reiniciarPartida();
    }
    public void salir()
    {
        gm.irMenuPrincipal();
    }
    public void salirPausa()
    {
        gm.outPausa();
    }
    public void updateStats(float vidaMax, float dano, float veloc, float critico, float velocAtk, float alcance, float xpMulti, float ptsMulti, float dineroMulti)
    {
        this.vidaMax.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(vidaMax * 100) / 100).ToString();
        this.atk.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(dano * 100) / 100).ToString();
        this.velocidad.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(veloc*100)/100).ToString();
        this.critico.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(critico * 100) / 100).ToString();
        this.velocAtk.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(velocAtk * 100) / 100).ToString();
        this.Alcance.GetComponent<TextMeshProUGUI>().text = "x"+alcance.ToString();
        this.XPMultiplier.GetComponent<TextMeshProUGUI>().text = "x" + xpMulti.ToString();
        this.PtsMultiplier.GetComponent<TextMeshProUGUI>().text = "x" + ptsMulti.ToString();
        this.DineroMultiplier.GetComponent<TextMeshProUGUI>().text = "x" + dineroMulti.ToString();
    }
}
