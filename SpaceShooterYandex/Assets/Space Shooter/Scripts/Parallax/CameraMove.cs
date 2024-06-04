using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// просто двигает камеру в одном направлении, имитируя движение игрока.
// Совершенно конкретный скрипт

public class CameraMove : MonoBehaviour {

    public float speed = 0.5f;
    public Transform verticalAnchor; // якорь для смещения по вертикали с отклонением от изначального положения
    public float verticalInhibitor = 0.05f; // замедлитель вертикального смещения
    float middleStart = 0; // отклонение по вертикали от изначальной точки
    bool scroll = false;

    void Awake () {
        middleStart = verticalAnchor.position.y;
    }

    public void ScrollOn () => scroll = true;
    public void ScrollOff () => scroll = false;

    void Update () {
        if (scroll) {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            pos.y = (verticalAnchor.position.y - middleStart) * verticalInhibitor;
            transform.position = pos;
        }
    }
}
