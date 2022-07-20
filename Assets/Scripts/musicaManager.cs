using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicaManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip musicaMenu;
    public AudioClip[] musicaGame;
    private void Start()
    {
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
