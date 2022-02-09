using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuyWood : MonoBehaviour
{
    private GameObject g;
    [SerializeField] 
    private GameObject woodPrefab;
    [SerializeField] 
    private Transform woodCreateArea;
    [SerializeField] 
    private Transform target;

    private GameObject[] woodArray;
    private bool isPlayercolliding = false;
    private float timer = 1f;
    public static int count = 0;


    private void Update()
    {
        if (isPlayercolliding == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>() != null)
        {
            isPlayercolliding = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>() != null && isPlayercolliding == true)
        {
            StartCoroutine(GenerateWoodToBuy());
            //count = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        StopCoroutine(GenerateWoodToBuy());
    }

    IEnumerator GenerateWoodToBuy()
    {
        if (count <= 15)
        {
            g = Instantiate(woodPrefab, woodCreateArea.position, woodCreateArea.rotation);
            count += 1;
            g.transform.DOMove(target.transform.position, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                WoodCollecter.Instance.WoodCollection();
            });
            yield return new WaitForSeconds(0.5f);

        }
    }
}
