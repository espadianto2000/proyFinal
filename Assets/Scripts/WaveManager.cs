using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Prefabs enemigos")]
    public GameObject[] prefabsEnemigosNiv0;
    public GameObject[] prefabsEnemigosNiv1;
    public GameObject[] prefabsEnemigosNiv2;
    public GameObject[] prefabsEnemigosNiv3;
    public GameObject[] prefabsEnemigosNiv4;
    public GameObject[] prefabsEnemigosNiv5;
    public GameObject[] prefabsEnemigosNiv6;
    public GameObject[] prefabsEnemigosNiv7;
    public GameObject[] prefabsEnemigosNiv8;
    public GameObject[] prefabsEnemigosNiv9;
    public GameObject[] prefabsEnemigosNiv10;
    public GameObject[] prefabsBosses;

    [Header("Wave")]
    public float timeTotal=0f;
    public float timeEntreOleadas = 4f;
    public float enemigosPorOleada = 1f;
    public int oleadaAnterior = 0;
    public int nivelDificultad = 0;
    public int cantidadMaximaEnemigos = 30;
    public int iteracionOleada = 1;
    public GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemigos();
        enemigosPorOleada = 2f;
        //player = GameObject.Find("Heroe");
    }

    // Update is called once per frame
    void Update()
    {
        timeTotal += Time.deltaTime;
        if (oleadaAnterior < Mathf.Floor(timeTotal / timeEntreOleadas))
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < cantidadMaximaEnemigos)
            {
                spawnEnemigos();
            }
            oleadaAnterior++;
        }
        if (Mathf.Floor(timeTotal / 360) > nivelDificultad)
        {
            if (nivelDificultad >= 10)
            {
                nivelDificultad = 0;
                iteracionOleada++;
                cantidadMaximaEnemigos = 30;
            }
            else { nivelDificultad++; }
            cantidadMaximaEnemigos+=5;
        }
    }
    public void spawnEnemigos()
    {
        
        for(int i = 0;i < Mathf.Floor(enemigosPorOleada); i++)
        {
            float x = 25;
            float y = 17.5f;
            switch (Random.Range(0, 4))
            {
                case 0:
                    x = x * -1f;
                    y = Random.Range(-15f, 15f);
                    break;
                case 1:
                    y = Random.Range(-15f, 15f);
                    break;
                case 2:
                    y = y * -1f;
                    x = Random.Range(-22f, 22f);
                    break;
                case 3:
                    x = Random.Range(-22f, 22f);
                    break;
            }
            switch (nivelDificultad)
            {
                case 0:
                    Instantiate(prefabsEnemigosNiv0[Random.RandomRange(0, prefabsEnemigosNiv0.Length)], new Vector2(player.transform.position.x+x, player.transform.position.y+y), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(prefabsEnemigosNiv1[Random.RandomRange(0, prefabsEnemigosNiv1.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(prefabsEnemigosNiv2[Random.RandomRange(0, prefabsEnemigosNiv2.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(prefabsEnemigosNiv3[Random.RandomRange(0, prefabsEnemigosNiv3.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(prefabsEnemigosNiv4[Random.RandomRange(0, prefabsEnemigosNiv4.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(prefabsEnemigosNiv5[Random.RandomRange(0, prefabsEnemigosNiv5.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 6:
                    Instantiate(prefabsEnemigosNiv6[Random.RandomRange(0, prefabsEnemigosNiv6.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 7:
                    Instantiate(prefabsEnemigosNiv7[Random.RandomRange(0, prefabsEnemigosNiv1.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 8:
                    Instantiate(prefabsEnemigosNiv8[Random.RandomRange(0, prefabsEnemigosNiv2.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 9:
                    Instantiate(prefabsEnemigosNiv9[Random.RandomRange(0, prefabsEnemigosNiv3.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
                case 10:
                    Instantiate(prefabsEnemigosNiv10[Random.RandomRange(0, prefabsEnemigosNiv4.Length)], new Vector2(player.transform.position.x + x, player.transform.position.y + y), Quaternion.identity);
                    break;
            }
        }
        enemigosPorOleada = (enemigosPorOleada * 1.2f) < 20 ? (enemigosPorOleada * 1.2f) : 20;
    }
}
