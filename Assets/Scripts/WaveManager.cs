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
    public GameObject[] prefabsBosses;

    [Header("Wave")]
    public float timeTotal=0f;
    public float timeEntreOleadas = 4f;
    public float enemigosPorOleada = 1f;
    public int oleadaAnterior = 0;
    public int nivelDificultad = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTotal += Time.deltaTime;
        if (oleadaAnterior < Mathf.Floor(timeTotal / timeEntreOleadas))
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < 50)
            {
                //spawnEnemigos();
            }
            oleadaAnterior++;
        }
        if (Mathf.Floor(timeTotal / 360) > nivelDificultad)
        {
            nivelDificultad++;
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
                    Instantiate(prefabsEnemigosNiv0[Random.RandomRange(0, prefabsEnemigosNiv0.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(prefabsEnemigosNiv1[Random.RandomRange(0, prefabsEnemigosNiv1.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(prefabsEnemigosNiv2[Random.RandomRange(0, prefabsEnemigosNiv2.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(prefabsEnemigosNiv3[Random.RandomRange(0, prefabsEnemigosNiv3.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(prefabsEnemigosNiv4[Random.RandomRange(0, prefabsEnemigosNiv4.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(prefabsEnemigosNiv5[Random.RandomRange(0, prefabsEnemigosNiv5.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
                case 6:
                    Instantiate(prefabsEnemigosNiv6[Random.RandomRange(0, prefabsEnemigosNiv6.Length)], new Vector2(x, y), Quaternion.identity);
                    break;
            }
        }
        enemigosPorOleada = (enemigosPorOleada * 1.2f) < 10 ? (enemigosPorOleada * 1.2f) : 10;
    }
}
