using UnityEngine;

[CreateAssetMenu(menuName = "Configs/StandardEnemyShip")]
public class StandardEnemyShipConfig : ScriptableObject
{
    [field: SerializeField] public int Health {  get; private set; }
}
