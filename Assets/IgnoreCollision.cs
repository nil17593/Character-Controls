using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    
    void Start()
    {
        Physics.IgnoreLayerCollision(0, 3);
    }


}
