using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WoodCollecter : MonoBehaviour
{
    private List<int> woodList = null;
    private int woodCount = 0;
    private float height = 0f;
    private float height2 = 0f;

    [Header("Collected wood by player")]
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject woodInstantiateArea;
    [SerializeField] private GameObject woodInstantiateArea2;

    private static WoodCollecter instance;
    public static WoodCollecter Instance { get { return instance; } }
    void Start()
    {
        instance = this;
        woodList = new List<int>();
    }


    public void WoodCollection()
    {
        Debug.Log("Top");
        if (woodCount <= 20)
        {
            Debug.Log("less");
            GameObject go = Instantiate(woodPrefab);
            woodCount++;
            woodList.Add(woodCount);
            if (woodCount <= 10)
            {
                go.transform.position = woodInstantiateArea.transform.position;
                go.transform.rotation = woodInstantiateArea.transform.rotation;
                go.transform.SetParent(woodInstantiateArea.transform);
                Vector3 tempPos = woodInstantiateArea.transform.position;
                tempPos.y += height;
                go.transform.position = tempPos;
                height += 0.9f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
                Debug.Log(woodCount);
            }

            else if (woodCount > 10)
            {
                go.transform.position = woodInstantiateArea2.transform.position;
                go.transform.rotation = woodInstantiateArea2.transform.rotation;
                go.transform.SetParent(woodInstantiateArea2.transform);
                Vector3 temp = woodInstantiateArea2.transform.position;// + new Vector3(0f, -2f, 0f);
                temp.y += height2;
                go.transform.position = temp;
                height2 += 0.8f;
                UIManager.Instance.value += 1;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
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
        }
    }

}