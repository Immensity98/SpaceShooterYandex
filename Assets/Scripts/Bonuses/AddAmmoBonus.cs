using DG.Tweening;
using TMPro;
using UnityEngine;

public class AddAmmoBonus : MonoBehaviour
{

    [SerializeField] private int _addedValue;

    public Player Player;
    public TextMeshProUGUI AmmoView;

    public float distance = 1f; // Расстояние, на которое нужно переместить объект
    public float duration = 1f; // Продолжительность перемещения

    private Vector3 _startPosition;

    public GameObject ToPosObject;
    public Vector3 currentPosition;

    private void Start()
    {
        _startPosition = transform.position;
        currentPosition = ToPosObject.transform.position;
    }

    private void Update()
    {
        DoMoveAmmoBounus();
    }

    public void DoMoveAmmoBounus()
    {
        if (transform.position.y == _startPosition.y)
            transform.DOMoveY(ToPosObject.transform.position.y, duration).OnComplete(() =>
            {
                transform.DOMoveY(_startPosition.y, duration);
            });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<Bullet>())
        {
            Player.AmmoValue += _addedValue;
            AmmoView.text = Player.AmmoValue.ToString();
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        _startPosition = transform.position;
    }
}

