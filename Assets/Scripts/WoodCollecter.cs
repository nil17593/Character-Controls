﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// this class handles the logic of wood collection 
/// the collected wood is instantiated in given area
/// the integer list is holds the counts of a collected wood 
/// </summary>
public class WoodCollecter : MonoBehaviour//IpooledObject
{
    #region Other scripts references
    public WoodSelling woodSelling;
    #endregion

    #region private Components
    private List<int> woodList;
    private int woodCount = 0;
    private float height = 0f;
    private float height2 = 0f;
    private GameObject wood;
    private List<GameObject> woods;
    private int count2;
    private Vector3 tempPos;
    private Vector3 temp;
    private bool isWoodCountEmpty;
    #endregion

    [Header("Collected wood by player")]
    [SerializeField]
    private GameObject WoodPrefab;
    [SerializeField]
    private GameObject WoodInstantiateArea;
    [SerializeField]
    private GameObject WoodInstantiateArea2;

    [Header("UI- Floating Point")]
    [SerializeField]
    private GameObject FloatingPoint;
    [SerializeField]
    private Transform FlotObject;

    public Transform woodCollectionArea;


    private static WoodCollecter instance;
    public static WoodCollecter Instance { get { return instance; } }//public instance to share
    void Start()
    {
        instance = this;
        woodList = new List<int>();
        woods = new List<GameObject>();
    }

    //wood collection by player 
    public void WoodCollection()
    {
        if (woods.Count <= 15)
        {
            wood = ObjectPooler.Instance.GetPooledObject();
            woods.Add(wood.gameObject);
            woodList.Add(1);
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
            if (woods.Count <= 8)
            {
                wood.transform.SetParent(WoodInstantiateArea.transform);
                wood.transform.position = WoodInstantiateArea.transform.position;
                wood.transform.rotation = WoodInstantiateArea.transform.rotation;              
                wood.SetActive(true);
                tempPos = WoodInstantiateArea.transform.position;
                tempPos.y += height;
                wood.transform.position = tempPos;
                height += 0.9f;
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
                //GenerateFloatingPoint();
                //DisableFloatingPoints();
            }

            else
            {
                wood.transform.SetParent(WoodInstantiateArea2.transform);
                wood.transform.position = WoodInstantiateArea2.transform.position;
                wood.transform.rotation = WoodInstantiateArea2.transform.rotation;              
                wood.SetActive(true);
                temp = WoodInstantiateArea2.transform.position;
                temp.y += height2;
                wood.transform.position = temp;
                height2 += 0.8f;
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
                //GenerateFloatingPoint();
                //DisableFloatingPoints();
            }
        }
        else
        {
            woodList.Add(1);
            Debug.Log("Wood Count= " + woods.Count);
            Debug.Log("WoodList Count= " + woodList.Count);
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
            Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
            //GenerateFloatingPoint();
            //DisableFloatingPoints();
        }
    }


    void GenerateFloatingPoint()
    {
        FloatingPoint = FloatingWoodObjectPooler.Instance.GetFloatingPooledObject();
        FloatingPoint.transform.position = FlotObject.transform.position;
        FloatingPoint.transform.rotation = Quaternion.identity;
        FloatingPoint.SetActive(true);
    }

    IEnumerator DisableFloatingPoints()
    {
        yield return new WaitForSeconds(2f);
        FloatingPoint.SetActive(false);
    }

    public void SellWood()
    {
        //if (woods.Count>0 || woodList.Count > 0 && woodSelling.isPlayercolliding)
        //{
        //    RemoveList();
        //}
        if (woodList.Count > 0)
        {
            RemoveList();
        }
        else
        {
            return;
        }
    }

    private void RemoveList()
    {
        woodList.RemoveAt(woodList.Count - 1);
        Debug.Log("WoodCount " + woodCount);
        UIManager.Instance.DecreaseScore(1);
        UIManager.Instance.IncreaseCoins(1);
        if (woodList.Count == 0)
        {
           StartCoroutine(DoAnimateWoods());
        }
    }

    
    public IEnumerator DoAnimateWoods()
    {
        for(int i = woods.Count-1; i >= 0; i--)
        {
            Debug.Log("i ="+i);
            //if(woodSelling.isPlayercolliding)
            //{
                yield return new WaitForSeconds(0.1f);
                woods[i].gameObject.transform.DOMove(woodCollectionArea.transform.position, 0.8f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    RemoveWoods();
                });
            //}
            //else
            //{
            //    yield return null;
            //}
        }
    }


    void RemoveWoods()
    {
        if (woods.Count > 0)
        {
            woods[woods.Count - 1].gameObject.SetActive(false);
            if (woods.Count <= 8)
            {
                height -= 0.9f;
            }
            if (woods.Count > 8)
            {
                height2 -= 0.8f;
            }
            woods.RemoveAt(woods.Count - 1);
        }
        Debug.Log("REMOVELIST FUNCTION  " + woodCount);
    }
}