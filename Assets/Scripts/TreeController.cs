using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TreeController : MonoBehaviour
{
    [HideInInspector]public int count;
    private float distance;
    public GameObject woodPrefab;
    public GameObject woodPrefab2;
    private GameObject childprefab;
    private float height;
    private bool logCreate = true;
    #region Collected wood 
    [Header("Collected wood by player")]
    public GameObject woodInstantiateArea;
    public GameObject woodInstantiateArea2;
    public Transform target;
    #endregion
    private static TreeController instance;
    public static TreeController Instance { get { return instance; } }


    private void Start()
    {
        instance = this;
    }
    IEnumerator DestroyWood(GameObject wood)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(wood);
        //wood.transform.DOKill();
        logCreate = true;
    }

    public void ReduceSize()
    {
        Vector3 reduceSize = new Vector3(0f, 0.1f, 0f);
        if (this.transform.localScale.y >= 0f)
        {
            this.transform.localScale -= reduceSize;

            if (logCreate == true)
            {
                DoAnimateWoods();
            }
        }
    }

    void DoAnimateWoods()
    {
        GameObject wood = Instantiate(woodPrefab);
        GameObject wood2 = Instantiate(woodPrefab);
        wood.gameObject.transform.position = this.transform.position + new Vector3(0f, -2f, 0f);
        wood2.gameObject.transform.position = this.transform.position + new Vector3(0f, -2f, 0f);

        wood.gameObject.transform.DOLocalJump(
            endValue: wood.transform.position + new Vector3(2f, 2f, 1.5f),
            jumpPower: 13f,
            numJumps: 1,
            duration: 1f).SetEase(Ease.OutBack).OnComplete(()=> {
                StartCoroutine(DestroyWood(wood));
                CollectWoods();
    });

        wood2.gameObject.transform.DOLocalJump(
           endValue: wood2.transform.position + new Vector3(-2f, 2f, 1.5f),
           jumpPower: 13f,
           numJumps: 1,
           duration: 1f).SetEase(Ease.OutBack).OnComplete(() =>
           {
               StartCoroutine(DestroyWood(wood2));
               CollectWoods();
           });
           logCreate = false;   
    }


    void CollectWoods()
    {
        GameObject go = Instantiate(woodPrefab2);
        WoodCollecter.woodCount++;
        WoodCollecter.woodList.Add(WoodCollecter.woodCount);
        //Debug.Log(WoodCollecter.woodCount) ;

        if (WoodCollecter.woodList.Count <= 8)
        {
            go.transform.position = woodInstantiateArea.transform.position;
            go.transform.rotation = woodInstantiateArea.transform.rotation;
            go.transform.SetParent(woodInstantiateArea.transform);
            Vector3 tempPos = woodInstantiateArea.transform.position;
            tempPos.y += height;
            go.gameObject.transform.position = tempPos;
            height += 0.7f;
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
        }
        //else if(WoodCollecter.woodList.Count >= 22)
        //{
        //    go.transform.position = woodInstantiateArea2.transform.position;
        //    go.transform.rotation = woodInstantiateArea2.transform.rotation;
        //    go.transform.SetParent(woodInstantiateArea2.transform);
        //    UIManager.Instance.value += 1;
        //    UIManager.Instance.pb.BarValue += 0.5f;
        //    UIManager.Instance.IncreaseScore(1);
        //}
        else
        {
            go.transform.position = woodInstantiateArea2.transform.position;
            go.transform.rotation = woodInstantiateArea2.transform.rotation;
            go.transform.SetParent(woodInstantiateArea2.transform);
            Vector3 temp = woodInstantiateArea2.transform.position;// + new Vector3(0f, -2f, 0f);
            temp.y += height;
            go.gameObject.transform.position = temp;
            height += 0.6f;
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
        }
       //Debug.Log(WoodCollecter.woodList.Count);
    }
}