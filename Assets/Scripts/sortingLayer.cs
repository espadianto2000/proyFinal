using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingLayer : MonoBehaviour
{
    public Renderer rd;
    void Start()
    {
        rd.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y * 100);
    }
}
