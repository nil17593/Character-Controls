using UnityEngine;


public class AxeController : MonoBehaviour
{
    private TreeController treeController;
    public float range;


    //private void FixedUpdate()
    //{
    //    //CheckIfRayCastHit();
    //}

    //void CheckIfRayCastHit()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position,Vector3.up, out hit,range))
    //    {
    //        Debug.DrawRay(transform.position,Vector3.up, Color.red);
    //        //Debug.Log(hit.collider.gameObject.name);
    //        TreeController tree = hit.transform.GetComponent<TreeController>();
    //        if (tree != null)
    //        {
    //            tree.ReduceSize();
    //        }
           
    //        //print(hit.collider.gameObject.name + "has been destroyed!");
    //        //Destroy(hit.collider.gameObject);
    //    }
    //}



    private void OnTriggerEnter(Collider other)
    {
        TreeController tree = other.transform.GetComponent<TreeController>();
        //Debug.Log(other.gameObject.name);
        if (tree != null)
        {
            tree.ReduceSize();
        }
    }



}