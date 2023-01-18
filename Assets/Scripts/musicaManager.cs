using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicaManager : MonoBehaviour
{
    public static musicaManager instance;
    public AudioSource music;
    public AudioClip musicaMenu;
    public AudioClip[] musicaGame;
    [SerializeField]
    private AudioSource audioNivel;
    [SerializeField]
    private AudioSource audioClick;
    private void Awake()
    {
        if (musicaManager.instance == null)
        {
            musicaManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameManager.instance.audioClick = audioClick;
        gameManager.instance.audioNivel = audioNivel;
        music = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(music.time >= music.clip.length - 1)
        {
            if(SceneManager.GetActiveScene().name == "menu")
            {
                music.time = 0;
                music.Play();
            }
            else
            {
                cambiarAJugador();
            }
        }
        if (Time.timeScale != 1)
        {
            music.volume = 0.075f;
        }
        else music.volume = 0.175f;
    }
    public void cambiarAMenu()
    {
        music.clip = musicaMenu;
        music.time = 0;
        music.Play();
    }
    public void cambiarAJugador()
    {
        music.clip = musicaGame[Random.Range(0, 11)];
        music.time = 0;
        music.Play();
    }
}
