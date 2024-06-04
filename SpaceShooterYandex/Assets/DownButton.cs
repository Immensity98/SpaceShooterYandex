using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Player Player;

    public bool isMove;

    private void Start () {
        isMove = false;
    }

    private void Update () {
        if (isMove == true) {
            Player.MoveDown();
        }
    }

    public void OnPointerDown (PointerEventData eventData) {
        isMove = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        isMove = false;
    }

}
