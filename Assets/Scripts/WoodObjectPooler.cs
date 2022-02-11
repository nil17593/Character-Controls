using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WoodObjectPooler : MonoBehaviour
{
    public static WoodObjectPooler sharedInstance;

    public List <GameObject> pooledObject;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        pooledObject = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObject.Add(obj);
        }
    }

    //public GameObject GetPooledObject()
    //{
    //    //1
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        //2
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    //3   
    //    return null;
    //}
    void Update()
    {

    }
}