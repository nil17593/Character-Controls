using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;
    //[SerializeField] private Transform secondTarget;
    //private CharacterMovement characterController;

    void FollowPlayer()
    {
        if(target != null)
        {
            Vector3 targetPos = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
            transform.position = targetPos;
        }
    }

    private void LateUpdate()
    {
        FollowPlayer();
        //StartCoroutine(LerpCameraToTheUI());
    }

    //public IEnumerator LerpCameraToTheUI()
    //{
    //    if (characterController.count == 2)
    //    {
    //        UIManager.Instance.coinPanel.SetActive(true);
    //        Vector3 targetPos2 = UIManager.Instance.coinPanel.transform.position + offset;   //target2.position + offset;
    //        Vector3 smoothPos2 = Vector3.Lerp(transform.position, targetPos2, smoothFactor * Time.deltaTime);
    //        transform.position = targetPos2;
    //        yield return new WaitForSeconds(3f);
    //        Vector3 targetPos = target.position + offset;
    //        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
    //        transform.position = targetPos;
    //        UIManager.Instance.coinPanel.SetActive(false);
    //    }
    //}
}
