using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public Player Player;

    public bool isMove;
    public Button UPButton;

    private void Start () {
        isMove = false;
    }
    
    private void Update () {
        //        Debug.Log(isMove);
        if (isMove == true) {
            Player.transform.Translate(0, Player._speed * Time.deltaTime, 0);
        }
    }
    public void OnPointerDown (PointerEventData eventData) {
        isMove = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        isMove = false;
    }
}


