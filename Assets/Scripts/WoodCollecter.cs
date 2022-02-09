using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// this class handles the logic of wood collection 
/// the collected wood is instantiated in given area
/// the integer list is holds the counts of a collected wood 
/// </summary>
public class WoodCollecter : MonoBehaviour
{
    #region private Components
    private List<int> woodList = null;
    private int woodCount = 0;
    private float height = 0f;
    private float height2 = 0f;
    private GameObject go;
    private List<GameObject> woods;
    private int count2;
    private Vector3 tempPos;
    private Vector3 temp;
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
        if (woodList.Count <= 16)
        {
            go = Instantiate(WoodPrefab);
            woods.Add(go.gameObject);
            woodList.Add(1);
            if (woods.Count <= 8)
            {
                go.transform.position = WoodInstantiateArea.transform.position;
                go.transform.rotation = WoodInstantiateArea.transform.rotation;
                go.transform.SetParent(WoodInstantiateArea.transform);
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
            woodList.Add(1);
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
            Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
            //height = 0f;
            //height2 = 0f;
            Debug.Log("1: "+height);
            Debug.Log("2: " + height2);

        }
    }

    public void SellWood()
    {
        if (woodList.Count > 0)
        {
            RemoveList();
            //ResetWoodInstantiation();
        }
    }

    private void RemoveList()
    {
        woodList.RemoveAt(woodList.Count - 1);
        UIManager.Instance.DecreaseScore(1);
        UIManager.Instance.IncreaseCoins(1);
        if (woodList.Count <= woods.Count)
        {
           StartCoroutine (DoAnimateWoods());
        }
    }

    
    private IEnumerator DoAnimateWoods()
    {
        for(int i = woods.Count - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            woods[i].gameObject.transform.DOMove(woodCollectionArea.transform.position, 0.8f).SetEase(Ease.InOutSine).OnComplete(() =>
              {
                  RemoveWoods();
              });
        }
    }


    void RemoveWoods()
    {
        Destroy(woods[woods.Count - 1].gameObject);
        if (woods.Count <= 8)
        {
            height -= 0.9f;
        }
        if (woods.Count > 8)
        {
            height2 -= 0.8f;
        }
        woods.RemoveAt(woods.Count - 1);

        Debug.Log("REMOVELIST FUNCTION  "+woodCount);
    }

    void ResetWoodInstantiation()
    {
        height = 0f;
        height2 = 0f;
        temp = WoodInstantiateArea2.transform.position;
        tempPos = woodCollectionArea.transform.position;
    }
}