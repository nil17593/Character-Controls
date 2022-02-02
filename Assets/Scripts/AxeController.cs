using UnityEngine;


public class AxeController : MonoBehaviour
{
    private TreeController treeController;
    TrailRenderer trail;
    public CharacterMovement character;

    private void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        Change();
    }

    private void OnTriggerEnter(Collider other)
    {
        TreeController tree = other.transform.GetComponent<TreeController>();
       
        if (tree != null)
        {
            tree.ReduceSize();
        }
    }

    public void ChangeTrailState(bool isEmmiting,float duration)
    {
        Debug.Log("Chaltay");
        trail.emitting = isEmmiting;
        trail.time = duration;
    }

    private void Change()
    {
        if (character.cutting == true)
        {
            trail.emitting = true;
            trail.time = 0.2f;
        }
        else
        {
            trail.emitting = false;
            trail.time = 0.2f;
        }
    }
}