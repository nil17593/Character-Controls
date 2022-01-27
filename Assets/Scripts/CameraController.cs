using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

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
    }

    public IEnumerator DisplayUnlockingLand()
    {
        Vector3 targetPos = target2.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
        transform.position = targetPos;

        yield return new WaitForSeconds(2f);

        Vector3 targetPos2 = target.position + offset;
        Vector3 smoothPos2 = Vector3.Lerp(transform.position, targetPos2, smoothFactor * Time.deltaTime);
        transform.position = targetPos2;
    }
}
