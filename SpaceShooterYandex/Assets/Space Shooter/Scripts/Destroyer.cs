using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private GameObject _player;

    public Animator Animator;

    private void Start()
    {
        _playerHealth.IsDie += PlayerDie;
    }

    public void PlayerDie()
    {
        Animator.SetTrigger("isDie");
    }
}
