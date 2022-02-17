using System.Collections;
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
    #region private Components
    private List<int> woodList;
    private int woodCount = 0;
    private float height = 0f;
    private float height2 = 0f;
    private GameObject go;
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
        if (woods.Count <= 16)
        {
            go = ObjectPooler.Instance.GetPooledObject();
            woods.Add(go.gameObject);
            //woodList.Add(1);
            woodCount += 1;
            Debug.Log("wqods Count =" + woods.Count);
            if (woods.Count <= 8)
            {
                go.transform.position = WoodInstantiateArea.transform.position;
                go.transform.rotation = WoodInstantiateArea.transform.rotation;
                go.transform.SetParent(WoodInstantiateArea.transform);
                go.SetActive(true);
                tempPos = WoodInstantiateArea.transform.position;
                tempPos.y += height;
                go.transform.position = tempPos;
                height += 0.9f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
            }

            else //if (woodCount > 10)
            {
                go.transform.position = WoodInstantiateArea2.transform.position;
                go.transform.rotation = WoodInstantiateArea2.transform.rotation;
                go.transform.SetParent(WoodInstantiateArea2.transform);
                go.SetActive(true);
                temp = WoodInstantiateArea2.transform.position;// + new Vector3(0f, -2f, 0f);
                temp.y += height2;
                go.transform.position = temp;
                height2 += 0.8f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
            }
        }
        else
        {
            //woods.Add(1);
            Debug.Log("Wood Count= " + woods.Count);
            Debug.Log("WoodList Count= " + woodList.Count);
            woodCount += 1;
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
            Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
        }
    }


    public void SellWood()
    {
        if (woodCount > 0 )
        {
            RemoveList();
        }
    }

    private void RemoveList()
    {
        woodCount -= 1;
        Debug.Log("WoodCount " + woodCount);
        UIManager.Instance.DecreaseScore(1);
        UIManager.Instance.IncreaseCoins(1);
        if (woodCount <= woods.Count)
        {
           StartCoroutine(DoAnimateWoods());
        }
    }

    
    public IEnumerator DoAnimateWoods()
    {
        for(int i = woods.Count-1; i > 0; i--)
        {
            Debug.Log("i ="+i);
            yield return new WaitForSeconds(0.1f);
            woods[i].gameObject.transform.DOMove(woodCollectionArea.transform.position, 0.8f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                  RemoveWoods();
            });
        }
    }


    void RemoveWoods()
    {
        if (woods.Count > 0 && woodCount<=woods.Count)
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