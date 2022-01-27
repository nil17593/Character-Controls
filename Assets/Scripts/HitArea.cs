using UnityEngine;

public class HitArea : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //TreeController treeController = other.transform.GetComponent<TreeController>();
        if(other.gameObject.GetComponent<TreeController>() != null)
        {
            characterMovement.Attack();
        }
        else
        {
            characterMovement.StopAttck();
        }
    }
}