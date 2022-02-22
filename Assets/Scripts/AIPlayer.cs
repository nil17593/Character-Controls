using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIPlayer : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private TreeController treeController;
        //[SerializeField]
        //private LayerMask whatisTree, whatIsGround;
        //[SerializeField]
        //private Transform Destination;
        //[SerializeField]
        private Rigidbody Rigidbody;

        private Animator animator;
        public bool cutting = false;
        public bool running;
        [SerializeField]
        private GameObject[] targets;
        private int i = 0;
        public bool reached = false;

        private float timer = 15f;


        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            targets = GameObject.FindGameObjectsWithTag("Tree");
            //navMeshAgent.destination = Destination.transform.position;
            //navMeshAgent.SetDestination(targets[i].transform.position);
            navMeshAgent.destination = targets[i].transform.position;
        }


        void Update()
        {
            float dist = Vector3.Distance(targets[i].transform.position, transform.position);
            if (dist < 5f)
            {
                i++;
                if (i < targets.Length)
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = targets[i].transform.position; //go to next target by setting it as the new destination
                }
            }
            if (!reached)
            {
                running = true;
                //cutting = false;
                animator.SetBool("AIRunning", true);
                animator.SetBool("AIIdle", false);
                animator.SetBool("AICutting", false);
            }
        }
        void Patrolling()
        {
            if (targets[i] == null)
            {
                navMeshAgent.SetDestination(targets[i + 1].transform.position);
            }
        }

        void CuttingTree()
        {
            //navMeshAgent.speed = 0f;
            if (cutting == true && reached==true && navMeshAgent.isStopped==true)
            {              
                //navMeshAgent.velocity.magnitude = null;
                animator.SetBool("AIIdle", false);
                animator.SetBool("AIRunning", false);
                animator.SetBool("AICutting", true);
            }
        }


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                cutting = true;
                reached = true;
                running = false;
                navMeshAgent.isStopped = true;
                //navMeshAgent.speed = 0f;
                //navMeshAgent.transform.LookAt(Destination.gameObject.transform.position);
                CuttingTree();
            }
            Debug.Log("yyy " + other.gameObject.name);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                cutting = false;
                animator.SetBool("AICutting", false);
            }
        }

        //private void OnCollisionStay(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag("Tree"))
        //    {
        //        cutting = true;
        //        reached = true;
        //        running = false;
        //        navMeshAgent.speed = 0f;
        //        navMeshAgent.transform.LookAt(collision.gameObject.transform.position);
        //        animator.SetBool("AIRunning", false);
        //        animator.SetBool("AIIdle", false);
        //        animator.SetBool("AICutting", true);
        //        //Rigidbody.GetComponent<Rigidbody>().isKinematic = false;
        //        //CuttingTree();
        //    }
        //    Debug.Log("yyy " + collision.gameObject.name);
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag("Tree"))
        //    {
        //        cutting = false;
        //        animator.SetBool("AICutting", false);
        //    }
        //}


        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (targets.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            navMeshAgent.destination = targets[i].transform.position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            i = (i + 1) % targets.Length;
        }
    }
}
