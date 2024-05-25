using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyBulletConfig")]
public class EnemyBulletConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField] public Vector2 MoveDirection {  get; private set; }
    [field: SerializeField] public float Speed {  get; private set; }  
}
