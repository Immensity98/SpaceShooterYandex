using TMPro;
using UnityEngine;

public class BulletsSpawner : Spawner
{
    public Player player;
    public TextMeshProUGUI AmmoView;
    public AudioSource _shotSound;
    public override void PoolElementActivator()
    {

        if (player.AmmoValue > 0 && _objectPool.HasFreeElement(out GameObject element))
        {
            Debug.Log("оскъ дспю");
            element.SetActive(true);

            _shotSound.Play();

            SetSpawnPosition(element);
            player.AmmoValue -= 1;
            AmmoView.text = player.AmmoValue.ToString();

        }

        else
        {
            Debug.Log("ObjectNet");
        }
    }
}
