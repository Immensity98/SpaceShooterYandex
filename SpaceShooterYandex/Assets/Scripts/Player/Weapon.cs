using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Weapon")]
public class Weapon : ScriptableObject
{
    [field: SerializeField] public int NumberOfBullets { get; private set; }   
    [field: SerializeField] public float ShotDelay { get; private set; } 
    [field: SerializeField] public int Damage { get; private set; }

}