using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bonusesList = new List<GameObject>();
    [SerializeField] private int _spawnDelay;

    [SerializeField] private GameObject AmmoBonus;
    [SerializeField] private GameObject HealthBonus;

    [SerializeField] protected List<GameObject> _spawnPointList = new List<GameObject>();

    private int currentSpawnPointIndex;

    private void Start()
    {
        _bonusesList.Add(AmmoBonus);
        _bonusesList.Add(HealthBonus);

        StartCoroutine("SpawnCoroutine");
    }

    IEnumerator SpawnCoroutine()
    {
        Debug.Log("Corotina");
        while (true)
        {
            SetSpawnPosition();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void SetSpawnPosition()
    {
        int bonusIndex = Random.Range(0, _bonusesList.Count);
        GameObject bonus = _bonusesList[bonusIndex];    
        currentSpawnPointIndex = Random.Range(0, _spawnPointList.Count);
        bonus.transform.position = _spawnPointList[currentSpawnPointIndex].transform.position;
        bonus.gameObject.SetActive(true);
    }
}
