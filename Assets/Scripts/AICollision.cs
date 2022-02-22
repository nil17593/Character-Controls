using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AICollision : MonoBehaviour
{
    [SerializeField]
    private GameObject myTarget;
    [SerializeField]
    private NavMeshAgent NavMeshAgent;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private BoxCollider BoxCollider; 


    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        NavMeshAgent.destination = myTarget.transform.position;
        BoxCollider = GetComponentInChildren<BoxCollider>();
    }

   
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward,out hit,15f);

        if (hit.collider.gameObject.CompareTag("Tree"))
        {
            //Debug.Log(collision.gameObject.name);
            Animator.SetBool("AIIdle", false);
            Animator.SetBool("AIRunning", false);
            Animator.SetBool("AICutting", true);
        }

        //if (NavMeshAgent.velocity.magnitude > 0f)
        //{
        //    Animator.SetBool("AIRunning", true);
        //}
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Tree"))
    //    {
    //        Debug.Log(collision.gameObject.name);
    //        Animator.SetBool("AIIdle", false);
    //        Animator.SetBool("AIRunning", false);
    //        Animator.SetBool("AICutting", true);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Tree"))
    //    {
    //        //Animator.SetBool("AIIdle", false);
    //        Animator.SetBool("AIRunning", true);
    //        Animator.SetBool("AICutting", false);
    //    }
    //}
}
