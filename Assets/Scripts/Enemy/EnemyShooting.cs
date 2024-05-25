using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public ShootingConfig EnemyShootingConfig;
    private float _timer;

    [SerializeField] private Pool _pool;
    [SerializeField] private Transform _bulletSpawnPosition;

    [SerializeField] private AudioSource _shotSound;

    private void Awake()
    {
        _pool = FindObjectOfType<EnemyBulletsPool>();
    }
    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > EnemyShootingConfig.ShotDelay && _pool.HasFreeElement(out GameObject element))
        {

            SetSpawnPosition(element);
            _shotSound.Play();
            _timer = 0;
        }
    }

    private void SetSpawnPosition(GameObject bullet)
    {       
        bullet.transform.position = _bulletSpawnPosition.position;
    }
}
