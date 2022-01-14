using System.Collections;
using UnityEngine;


public class TreeController : MonoBehaviour
{
    //#region Obstacle
    //[Header("Tree")]
    //public GameObject Obstacle;
    //private Rigidbody treeRig;
    //public GameObject Obstacle2;
    //public GameObject Obstacle3;
    //public GameObject Obstacle4;
    //#endregion

    //private CharacterMovement characterMovement;
    private static TreeController instance;
    public static TreeController Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
    }

    public void ReduceSize()
    {
        Vector3 reduceSize = new Vector3(0f, 1f, 0f);
        if (this.transform.localScale.y > 3f)
        {
            this.transform.localScale -= reduceSize * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Axe"))
        {
            ReduceSize();
        }
    }

}