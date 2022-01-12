using System.Collections;
using UnityEngine;


public class TreeController : MonoBehaviour
{
    //#region Obstacle
    //[Header("Tree")]
    //public GameObject Obstacle;
    //public GameObject Obstacle2;
    //public GameObject Obstacle3;
    //public GameObject Obstacle4;
    //#endregion

    private CharacterMovement characterMovement;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 reducesize = new Vector3(0f, 1f, 2f);
        if(collision.gameObject.CompareTag("Axe") && this.transform.localScale.y > 3f)
        {
            this.transform.localScale -= reducesize;
        }
    }





}