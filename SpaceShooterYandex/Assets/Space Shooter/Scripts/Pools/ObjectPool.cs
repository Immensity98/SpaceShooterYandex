using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [SerializeField] protected GameObject _objectPrefab;
    [SerializeField] protected int _poolSize;
    [SerializeField] public List<GameObject> _objectsList = new List<GameObject>();
    [SerializeField] private Transform _spawnContainer;

    private void Awake () {
        CreatePool(_poolSize);
    }

    private void CreatePool (int poolSize) {
        for (int i = 0; i < poolSize; i++) CreateObject();
    }

    private void CreateObject (bool ActiveByDefault = false) {
        GameObject createdObject = Instantiate(_objectPrefab, _spawnContainer); // îáúåêòû ñïàâíÿòñÿ â òî÷êå 0, 0, 0. Àêêóðàòíåå ñ èõ âêëþ÷åíèåì!
        createdObject.SetActive(ActiveByDefault);
        _objectsList.Add(createdObject);
    }

    public virtual bool HasFreeElement (out GameObject element) {
        foreach (GameObject poolObject in _objectsList) {
            if (!poolObject.activeInHierarchy) {
                element = poolObject;
                poolObject.SetActive(true);
                return true;
            }
        }
        element = null;
        throw new System.Exception("Íåò ñâîáîäíûõ îáüåêòîâ â ïóëå");
    }
}
