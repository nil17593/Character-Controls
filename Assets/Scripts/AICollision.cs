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
    private GameObject[] targets;
    private int i=4;
    public static bool iscollide;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.destination = targets[i].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            iscollide = true;
            NavMeshAgent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            iscollide = false;
            NavMeshAgent.isStopped = false;
        }
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
