using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
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
}
