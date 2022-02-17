using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> pooledObjects;
    public GameObject objectToiPool;
    public int amountToPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i <= amountToPool; i++)
        {
            temp = Instantiate(objectToiPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
