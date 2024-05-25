using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Vector2 _moveDirection;
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection.x, 0, 0);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
