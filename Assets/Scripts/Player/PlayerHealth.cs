using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action IsDie;
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Health {  get; private set; }

    public TextMeshProUGUI HealthView;

    public AudioSource DeathSound;

    private void Start()
    {
        Debug.Log(Health);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage");

        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }

        Debug.Log(Health);
    }

    public void AddHealth(int value)
    {
        if(Health < MaxHealth)
        {
            Health += value;
            HealthView.text = Health.ToString();
        }
    }

    public void Die()
    {
        IsDie?.Invoke();
    }

    public void SoundDestroy()
    {
        DeathSound.Play();
    }
}
