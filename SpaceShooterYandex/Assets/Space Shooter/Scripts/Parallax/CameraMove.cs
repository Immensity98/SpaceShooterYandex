using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// просто двигает камеру в одном направлении, имитируя движение игрока.
// Совершенно конкретный скрипт

public class CameraMove : MonoBehaviour {

    public float speed = 0.5f;

    void Update () {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
