using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuyWood : MonoBehaviour
{
    //private List<GameObject> woodstobuy;
    private GameObject g;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private Transform woodCreateArea;
    [SerializeField] private Transform target;
    private GameObject[] woodArray;
    private void Start()
    {
        //woodArray = new GameObject[g];
        //woodstobuy = new List<GameObject>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>() != null)
        {
            StartCoroutine(GenerateWoodToBuy());

        }
    }

    IEnumerator GenerateWoodToBuy()
    {
        for(int i = 0; i < 14; i++)
        {
            g =Instantiate(woodPrefab,woodCreateArea.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);

            g.transform.DOMove(target.transform.position, 1f).SetEase(Ease.InOutSine).OnComplete(()=>{
                
                WoodCollecter.Instance.WoodCollection();
            });
        }
    }

   

}
