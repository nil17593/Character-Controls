using UnityEngine;


public class AxeController : MonoBehaviour
{
    private TreeController treeController;
    public float range;


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