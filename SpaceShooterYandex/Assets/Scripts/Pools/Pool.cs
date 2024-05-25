using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolSize;
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _objectsContainer;

    [SerializeField] private List<GameObject> _pool = new List<GameObject>();


    private void Awake()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreateObject();
        }
    }

    public void CreateObject()
    {
        GameObject createdObject = Instantiate(_object, _objectsContainer);
        createdObject.SetActive(false);
        _pool.Add(createdObject);
    }

    public virtual bool HasFreeElement(out GameObject element)
    {
        foreach (GameObject poolObject in _pool)
        {
            if (!poolObject.activeInHierarchy)
            {
                element = poolObject;
                poolObject.SetActive(true);
                return true;
            }
        }

        throw new System.Exception();
    }
}
