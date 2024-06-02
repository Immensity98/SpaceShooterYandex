using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=zit45k6CUMk

//If anyone is using Cinemachine and is having a glitch where the background sprite doesn't properly loop when you reach the end, try changing 
//length = GetComponent<SpriteRenderer>().bounds.size.x;  in the start method to length = GetComponent<SpriteRenderer>().size.x;


public class Parallax : MonoBehaviour {

    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect; // Запаздывание фона. Чем ближе к 1, тем медленнее запаздывает фон. Если 1, то фон движется синхронно с камерой.


    void Start () {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update () {
        float distance = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x * (1 - parallaxEffect); // Скорость набегания на край обратно пропорциональна запаздыванию фона!
        transform.position = new Vector2(startpos + distance, transform.position.y);
        // перекладка
        if (temp > startpos + length) startpos += length; else if (temp < startpos - length) startpos -= length;
    }
}
