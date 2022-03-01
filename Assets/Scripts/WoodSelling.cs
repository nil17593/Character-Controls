using UnityEngine;
using AI;

public class WoodSelling : MonoBehaviour
{
    public bool isPlayercolliding = false;

    private float timer = 1f;

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


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<CharacterMovement>() != null && isPlayercolliding)
        {
            WoodCollecter.Instance.SellWood();
            BuyWood.count = 0;
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        isPlayercolliding = false;
        StopCoroutine(WoodCollecter.Instance.DoAnimateWoods());
    }
}
