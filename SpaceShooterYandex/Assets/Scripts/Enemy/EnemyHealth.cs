using System;
using System.Threading;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [field: SerializeField] public int Health { get; private set; }

    public Score Score;
    public int Bounty;

    public AnimationEvent AnimationEvent;
    private int _currentHealth;
    private static int IsDie = Animator.StringToHash("isDie");

    public AudioSource AudioSource;

    public Animator Animator;

    private void Start () {
        _currentHealth = Health;
        Score = FindObjectOfType<Score>();
    }
    public void TakeDamage (int damage) {
        Health -= damage;
    }


    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Player")) {

            TakeDamage(1);

            if (Health <= 0) {
                AudioSource.Play();
                Animator.SetTrigger(IsDie);
                Health = _currentHealth;
                Score.AddScore(Bounty);
            }
            //            Debug.Log("EnemyTakeDamage Current HP = " + Health);
        }
    }

    public void SetActiveFalse () {
        gameObject.SetActive(false);
    }
}
