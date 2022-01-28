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
    public Transform woodInstantiateArea2;
    private List<GameObject> woodList = new List<GameObject>();
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
        Vector3 reduceSize = new Vector3(0f, 1f, 0f);
        if (this.transform.localScale.y >= 1f)// && time > Time.time)
        {
            this.transform.localScale -= reduceSize;

            if (logCreate == true)
            {
                DoAnimateWoods();
                CollectWoods();              
            }
        }
        Debug.Log(woodList.Count);
    }

    void DoAnimateWoods()
    {
        GameObject wood = Instantiate(woodPrefab);
        wood.gameObject.transform.position = this.transform.position + new Vector3(0f, -2f, 0f);
        //wood.gameObject.transform.DOLocalMove(woodInstantiateArea.gameObject.transform.position, 1f);//.SetEase(Ease.InFlash);

        //need to refine it
        wood.gameObject.transform.DOLocalJump(
            endValue: new Vector3(transform.position.x-2f, 1f, 2f),
            jumpPower: 15,
            numJumps: 1,
            duration: 0.5f).SetEase(Ease.InOutSine);
        //till this.
        logCreate = false;
        StartCoroutine(DestroyWood(wood));
    }

    void CollectWoods()
    {
        GameObject go = Instantiate(woodPrefab2, woodInstantiateArea.transform);
        woodList.Add(go);
        Debug.Log(go.transform.position);
        if (woodList.Count >= 5)
        {
            Debug.Log("hai");
            go.transform.position = woodInstantiateArea2.transform.position;
            go.transform.rotation = woodInstantiateArea2.transform.rotation;
            go.transform.SetParent(woodInstantiateArea2.transform);
            Vector3 temp = woodInstantiateArea2.transform.position+new Vector3(0f,-2f,0f);
            temp.y += height;
            go.gameObject.transform.position = temp;
            height += 0.5f;
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
        }
        else
        {
            go.transform.rotation = woodInstantiateArea.transform.rotation;
            go.transform.SetParent(woodInstantiateArea.transform);
            Vector3 tempPos = woodInstantiateArea.transform.localPosition;
            tempPos.x += height;
            go.gameObject.transform.localPosition = tempPos;
            height += 0.5f;
            UIManager.Instance.value += 1;
            UIManager.Instance.pb.BarValue += 0.5f;
            UIManager.Instance.IncreaseScore(1);
        }
    }


    public void SellWood()
    {
        for (int i = woodList.Count; i >= 0; i--)
        {
            woodList.RemoveAt(i);
            Destroy(woodPrefab);
        }
    }
}