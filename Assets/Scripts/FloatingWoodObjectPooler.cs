using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingWoodObjectPooler : MonoBehaviour
{

    public static FloatingWoodObjectPooler Instance;
    public List<GameObject> pooledObjects;
    public List<GameObject> pooledObjects2;
    public GameObject objectToPool;
    public GameObject objectToPool2;
    public int amountToPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        pooledObjects2 = new List<GameObject>();
        GameObject temp;
        GameObject temp2;
        for (int i = 0; i <= amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
        for (int i = 0; i <= amountToPool; i++)
        {
            temp2 = Instantiate(objectToPool2);
            temp2.SetActive(false);
            pooledObjects2.Add(temp2);
        }
    }

    public GameObject GetFloatingPooledObject()
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
    public GameObject GetFloatingPooledObject2()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects2[i].activeInHierarchy)
            {
                return pooledObjects2[i];
            }
        }
        return null;
    }
}