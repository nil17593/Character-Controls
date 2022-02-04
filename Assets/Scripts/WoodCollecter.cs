using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private static WoodCollecter instance;
    public static WoodCollecter Instance { get { return instance; } }//public instance to share
    void Start()
    {
        instance = this;
        woodList = new List<int>();
    }

    //wood collection by player 
    public void WoodCollection()
    {
        Debug.Log("Top");
        if (woodCount <= 16)
        {
            Debug.Log("less");
            go = Instantiate(WoodPrefab);
            woodCount++;
            woodList.Add(woodCount);
            if (woodCount <= 8)
            {
                go.transform.position = WoodInstantiateArea.transform.position;
                go.transform.rotation = WoodInstantiateArea.transform.rotation;
                go.transform.SetParent(WoodInstantiateArea.transform);
                Vector3 tempPos = WoodInstantiateArea.transform.position;
                tempPos.y += height;
                go.transform.position = tempPos;
                height += 0.9f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
                Debug.Log(woodCount);
            }

            else //if (woodCount > 10)
            {
                go.transform.position = WoodInstantiateArea2.transform.position;
                go.transform.rotation = WoodInstantiateArea2.transform.rotation;
                go.transform.SetParent(WoodInstantiateArea2.transform);
                Vector3 temp = WoodInstantiateArea2.transform.position;// + new Vector3(0f, -2f, 0f);
                temp.y += height2;
                go.transform.position = temp;
                height2 += 0.8f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
                Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
                Debug.Log(woodCount);
            }
        }
        else
        {
            woodCount++;
            woodList.Add(woodCount);
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
            Instantiate(FloatingPoint, FlotObject.transform.position, Quaternion.identity);
        }
    }

}