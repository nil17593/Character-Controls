using UnityEngine;


public class AxeController : MonoBehaviour
{
    #region refereance of other scripts
    private TreeController treeController;
    #endregion

    #region private components
    private TrailRenderer trail;
    #endregion

    #region player reference for trail emitting
    [SerializeField]
    private CharacterMovement character;
    #endregion

    private void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        ChangeTrailState();
    }

    //reduce tree size while trigger with tree
    private void OnTriggerEnter(Collider other)
    {
        TreeController tree = other.transform.GetComponent<TreeController>();
       
        if (tree != null)
        {
            tree.ReduceSize();
        }
    }


    //Change trail state while cutting tree
    private void ChangeTrailState()
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