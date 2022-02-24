using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIPlayerController : MonoBehaviour
    {

        public Transform[] waypoints;
        private int destination = 0;
        private Animator anim;
        private NavMeshAgent agent;

        void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            GotoNextPoint();
        }
        void GotoNextPoint()
        {
            if (waypoints.Length == 0)
                return;
            agent.destination = waypoints[destination].position;
            destination = (destination + 1) % waypoints.Length;
        }
        void Update()
        {
            anim.SetFloat("Running", agent.velocity.magnitude);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
        void OnTriggerEnter(Collider col)
        {
            if (col.transform.CompareTag("Tree"))
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                transform.LookAt(col.transform);
            }
        }

        void OnTriggerStay(Collider col)
        {
            if (col.transform.CompareTag("Tree"))
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                transform.LookAt(col.transform);
            }
        }
        void OnTriggerExit(Collider col)
        {
            agent.isStopped = false;
        }
    }
}