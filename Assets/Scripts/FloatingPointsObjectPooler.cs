using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingPointsObjectPooler : MonoBehaviour
{
    public static FloatingPointsObjectPooler Instance;
    public GameObject flaotingPreefab;
    public List<GameObject> pooledObjects;
    public int amountToPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i <= amountToPool; i++)
        {
            temp = Instantiate(flaotingPreefab);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }


}