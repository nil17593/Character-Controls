using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;//target for camera
    [SerializeField]
    private Vector3 offset;//distance between camera and player
    [SerializeField]
    private float smoothFactor;//soothfactor for camera


    //follow player
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
