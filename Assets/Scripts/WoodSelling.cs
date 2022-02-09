using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSelling : MonoBehaviour
{
   
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<CharacterMovement>() != null)
        {
            WoodCollecter.Instance.SellWood();
            BuyWood.count = 0;
        }
    }
}
