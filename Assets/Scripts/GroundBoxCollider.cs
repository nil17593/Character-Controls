using System.Collections;
using UnityEngine;


public class GroundBoxCollider : MonoBehaviour
{

    public static BoxCollider groundBoxCollider;

    private void Awake()
    {
        groundBoxCollider = GetComponent<BoxCollider>();
    }
}