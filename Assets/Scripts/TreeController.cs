using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


/// <summary>
/// Handles Tree logic
/// Attched on every tree in scene.
/// </summary>
public class TreeController : MonoBehaviour
{
    [Header("wood prefabs to animate")]
    [SerializeField]
    private GameObject woodPrefab;
    [SerializeField]
    private GameObject woodPrefab2;
    [SerializeField]
    private Transform target;

    #region private components and variables
    private bool logCreate = true;
    //private CapsuleCollider boxCollider;
    #endregion


    private static TreeController instance;
    public static TreeController Instance { get { return instance; } }


    private void Start()
    {
        instance = this;
        //boxCollider = GetComponentInChildren<CapsuleCollider>();
    }

    //coroutine for destroy woods
    IEnumerator DestroyWood(GameObject wood)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(wood);
        logCreate = true;
    }

    //reduce tree size after every hit
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
            //if (transform.localScale.y <= 2f)
            //{
            //    Debug.Log("aya");
            //    boxCollider = null;
            //}
        }
    }

    //animate woods using doTween
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
                WoodCollecter.Instance.WoodCollection();
    });

        wood2.gameObject.transform.DOLocalJump(
           endValue: wood2.transform.position + new Vector3(-2f, 2f, 1.5f),
           jumpPower: 13f,
           numJumps: 1,
           duration: 1f).SetEase(Ease.OutBack).OnComplete(() =>
           {
               StartCoroutine(DestroyWood(wood2));
               WoodCollecter.Instance.WoodCollection();
           });
           logCreate = false;   
    }

    void MoveTowardsTarget(GameObject wood)
    {
        if (target == null)
        {
            return;
        }
        else
        {
            wood.transform.DOMove(target.transform.position, 1f).SetEase(Ease.InSine).OnComplete(()=> {
                ChangePosition(wood);
            });
        }
    }

    void ChangePosition(GameObject wood)
    {
        wood.transform.position = this.transform.position;
    }
}