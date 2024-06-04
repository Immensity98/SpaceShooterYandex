using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private EnemyBulletConfig _enemyBulletsConfig;

    void Update () {
        transform.Translate(_enemyBulletsConfig.Speed * Time.deltaTime * _enemyBulletsConfig.MoveDirection.x, 0, 0);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (collision != null && collision.TryGetComponent<EnemyHealth>(out enemyHealth)) {
            enemyHealth.TakeDamage(1);
            gameObject.SetActive(false);
        }
    }

}
