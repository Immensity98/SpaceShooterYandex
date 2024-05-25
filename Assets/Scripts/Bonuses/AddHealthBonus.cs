using DG.Tweening;
using UnityEngine;

public class AddHealthBonus : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private int _addedValue;

    public float distance = 1f; // Расстояние, на которое нужно переместить объект
    public float duration = 1f; // Продолжительность перемещения

    private Vector3 _startPosition;

    public GameObject ToPosObject;

    private void Start()
    {
        _startPosition.y = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponent<Bullet>())
        {
            _playerHealth.AddHealth(_addedValue);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        DoMoveHealthBounus();
    }

    public void DoMoveHealthBounus()
    {
        if (transform.position.y == _startPosition.y)
            transform.DOMoveY(ToPosObject.transform.position.y, duration).OnComplete(() =>
            {
                transform.DOMoveY(_startPosition.y, duration);
            });
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
