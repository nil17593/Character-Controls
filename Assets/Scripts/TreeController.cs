using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TreeController : MonoBehaviour
{
    //#region Obstacle
    //[Header("Tree")]
    //public GameObject Obstacle;
    //private Rigidbody treeRig;
    //public GameObject Obstacle2;
    //public GameObject Obstacle3;
    //public GameObject Obstacle4;
    //#endregion
    //[SerializeField] private Transform Axe;
    [HideInInspector]public int count;
    private float distance;
    public GameObject woodPrefab;
    private GameObject childprefab;
    public Transform Boy;
    private float height;
    private bool logCreate = true;
    #region Collected wood 
    [Header("Collected wood by player")]

    public GameObject woodInstantiateArea;
    private List<GameObject> woodList = new List<GameObject>();
    //public Transform woodInstantiateArea2;
    //public Transform woodInstantiateArea3;
    //public Transform woodInstantiateArea4;
    //public GameObject woodsellArea;
    #endregion
    IEnumerator DestroyWood(GameObject wood)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(wood);
        logCreate = true;
    }

    public void ReduceSize()
    {
        Vector3 reduceSize = new Vector3(0f, 1f, 0f);
        if (this.transform.localScale.y >= 0f)// && time > Time.time)
        {
            this.transform.localScale -= reduceSize;

            if (logCreate == true)
            {
                GameObject wood = Instantiate(woodPrefab);
                wood.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 6, 2);
                wood.gameObject.transform.DOMove(woodInstantiateArea.gameObject.transform.position, 1f);
                logCreate = false;
                woodList.Add(wood);
                StartCoroutine(DestroyWood(wood));

                UIManager.Instance.value += 1;
                GameObject go = Instantiate(woodPrefab, woodInstantiateArea.transform);
                go.transform.rotation = woodInstantiateArea.transform.rotation;
                go.transform.SetParent(woodInstantiateArea.transform);
                Vector3 tempPos = woodInstantiateArea.transform.localPosition + new Vector3(0, 0, 0.5f);
                tempPos.x += height;
                go.gameObject.transform.localPosition = tempPos;
                height += 0.5f;
                UIManager.Instance.pb.BarValue += 0.5f;
                UIManager.Instance.IncreaseScore(1);
            }
        }
        Debug.Log(woodList.Count);
    }

   //public void InstantiateWood()
   //{  
   //     Destroy(childprefab);
   //     GameObject go = Instantiate(woodPrefab, woodInstantiateArea.transform);
   //     go.transform.SetParent(woodInstantiateArea.transform);  // GameObject.FindGameObjectWithTag("parent object").transform;                                                                        //count += 1;
        
        
   //     //childprefab.transform.position = Random.Range();
   //     //childprefab.transform.Rotate(new Vector3(90f, 0f, 0f));
   //     //childprefab.transform.rotation = woodInstantiateArea.transform.rotation;
   //                                                                      //Debug.Log(count);
   //     height += 0.2f;
        
   //}   
}