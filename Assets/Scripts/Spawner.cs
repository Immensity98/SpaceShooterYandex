using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPool _objectPool;

    private int currentSpawnPointIndex;

    [SerializeField] protected float _spawnDelay;
    [SerializeField] protected GameObject _objectPrefab;
    [SerializeField] protected GameObject _spawnPoint;
    [SerializeField] protected List<GameObject> _spawnPointList = new List<GameObject>();


    public virtual void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            PoolElementActivator();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public virtual void PoolElementActivator()
    {
        if (_objectPool.HasFreeElement(out GameObject element))
        {
            element.SetActive(true);

            SetSpawnPosition(element);
        }
    }

    public void SetSpawnPosition(GameObject ship)
    {
        currentSpawnPointIndex = Random.Range(0, _spawnPointList.Count);
        ship.transform.position = _spawnPointList[currentSpawnPointIndex].transform.position;
    }
}
