using System.Collections;
using UnityEngine;
using DG.Tweening;
using AI;
using System.Collections.Generic;


/// <summary>
/// Handles Tree logic
/// Attched on every tree in scene.
/// </summary>
public class TreeController : MonoBehaviour
{
    #region private components and variables
    private bool logCreate = true;
    //private CapsuleCollider boxCollider;
    #endregion

    private GameObject wood;
    private GameObject wood2;
    public bool isNULL = false;
    public static bool isCollectedBYAI=false;
    //private bool isCollectedByPlayer=false;
    //private static TreeController instance;
    //public static TreeController Instance { get { return instance; } }
 

    private void Start()
    {
        isNULL = false;
        
        //instance = this;
    }


    //coroutine for destroy woods
    IEnumerator DestroyWood(GameObject wood)
    {
        yield return new WaitForSeconds(0.4f);
        wood.SetActive(false);
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
        }
        if (this.transform.localScale.y < 0.2f)
        {
            Debug.Log("LOCALSCALE Y= " + transform.localScale.y);
            //isNULL = true;
            //AIPlayer.instance.NextTarget();
        }
    }


    public void ReduceSizeForAI()
    {
        Vector3 reduceSize = new Vector3(0f, 0.1f, 0f);
        //isNULL = false;
        if (this.transform.localScale.y >= 0f)
        {
            this.transform.localScale -= reduceSize;
            if (logCreate == true)
            {
                DoAnimateWoods();
            }
            if (this.transform.localScale.y <= 0.2f)
            {
                Debug.Log("LOCALSCALE Y= " + transform.localScale.y);
                isNULL = true;
                AIPlayer.instance.NextTarget();
            }
        }

    }

    //animate woods using doTween
    void DoAnimateWoods()
    {
        wood = FloatingWoodObjectPooler.Instance.GetFloatingPooledObject();
        wood2 = FloatingWoodObjectPooler.Instance.GetFloatingPooledObject2();
        wood.gameObject.transform.position = this.transform.position + new Vector3(0f, -2f, 0f);
        wood.SetActive(true);
        wood2.gameObject.transform.position = this.transform.position + new Vector3(0f, -2f, 0f);
        wood2.SetActive(true);

        wood.gameObject.transform.DOLocalJump(
            endValue: wood.transform.position + new Vector3(2f, 2f, 1.5f),
            jumpPower: 13f,
            numJumps: 1,
            duration: 1f).SetEase(Ease.OutBack).OnComplete(()=> {
                StartCoroutine(DestroyWood(wood));
                //Wood1MoveTowardsPlayer(target);
                //if (isCollectedBYAI)
                //{
                //    AIWoodCollecter.Instance.WoodCollection();
                //}
                //else if(isCollectedByPlayer)
                //{
                    //Wood2MoveTowardsPlayer(target);
                    WoodCollecter.Instance.WoodCollection();
                //}
            });

        wood2.gameObject.transform.DOLocalJump(
           endValue: wood2.transform.position + new Vector3(-2f, 2f, 1.5f),
           jumpPower: 13f,
           numJumps: 1,
           duration: 1f).SetEase(Ease.OutBack).OnComplete(() =>
           {
               StartCoroutine(DestroyWood(wood2));
               //if (isCollectedBYAI)
               //{
               //    AIWoodCollecter.Instance.WoodCollection();
               //}
               //else if(isCollectedByPlayer)
               //{
                   //Wood2MoveTowardsPlayer(target);
                   WoodCollecter.Instance.WoodCollection();
               //}
           });
           logCreate = false;   
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AIAxeController>() != null)
        {
            AIWoodCollecter.Instance.WoodCollection();           
        }
        //else if (other.gameObject.GetComponent<AxeController>() != null)
        //{
        //    isCollectedByPlayer = true;
        //}
    }

    //void Wood1MoveTowardsPlayer(Transform target)
    //{
    //    float dist = Vector3.Distance(target.transform.position ,wood.transform.position);
    //    Debug.Log("Dist1= " + dist);
    //    if (dist < 10f)
    //    {
    //        wood.transform.DOMove(target.transform.position, 0.5f).SetEase(Ease.OutBack).OnComplete(()=> {
    //            wood.gameObject.SetActive(false);
    //        });
    //    }

    //    else
    //    {
    //        wood.transform.DORotate(wood.transform.position, 5f, RotateMode.Fast);
    //    }
    //} 

    //void Wood2MoveTowardsPlayer(Transform target)
    //{
    //    float dist2 = Vector3.Distance(target.transform.position, wood2.transform.position);
    //    Debug.Log("Dist2= " + dist2);

    //    if (dist2 < 10f)
    //    {
    //        wood2.transform.DOMove(target.transform.position, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
    //            wood2.gameObject.SetActive(false);
    //        });
    //    }
    //    wood2.transform.DORotate(wood2.transform.position, 5f, RotateMode.Fast);
    //}
}