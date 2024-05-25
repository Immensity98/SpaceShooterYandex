using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyShootingConfig")]
public class ShootingConfig : ScriptableObject
{
    [field: SerializeField] public float ShotDelay { get; private set; }
}
