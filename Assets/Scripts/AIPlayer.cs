using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIPlayer : MonoBehaviour
    {
        [Header("AI Player Components")]
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private TreeController treeController;
        private Rigidbody Rigidbody;
        private Animator animator;
        public bool cutting = false;
        public bool running;
        [SerializeField]
        private GameObject[] targets;
        public int i = 4;
        public bool reached = false;
        //private float timer = 15f;
        //private Rigidbody rb;
        public GameObject t1;
        public GameObject t2;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            //transform.position = navMeshAgent.nextPosition;
            navMeshAgent.SetDestination(t1.transform.position);
            //NextTarget();
        }

        void FixedUpdate()
        {
            //float dist = Vector3.Distance(transform.position, targets[i].transform.position);
            //if (dist <= 10f)
            //{
            //    navMeshAgent.isStopped = true;
            //    reached = true;
            //    cutting = true;
            //    CuttingTree();
            //}
            NextTarget();
            MovingTowardsTarget();           
        }

        void MovingTowardsTarget()
        {
            if (!reached)
            {
                running = true;
                cutting = false;
                navMeshAgent.isStopped = false;
                animator.SetBool("AIRunning", true);
                animator.SetBool("AIIdle", false);
                animator.SetBool("AICutting", false);
            }
        }

        void NextTarget()
        {
            if (TreeController.isNULL == true)
            {
                Debug.Log("II= " + i);
                reached = false;
                running = true;
                navMeshAgent.isStopped = false;
                Debug.Log("NAV= " + navMeshAgent.isStopped);
                //TreeController.isNULL = false;
                //i = (i + 1) % targets.Length;
                navMeshAgent.SetDestination(t2.transform.position);
                //if (targets.Length == 0)
                //    return;
                //navMeshAgent.destination = targets[i].transform.position;
                //i = (i + 1) % targets.Length;
            }
        }

        public void CuttingTree()
        {
            //navMeshAgent.speed = 0f;
            if (cutting == true && reached==true  && running == false)
            {
                animator.SetBool("AIIdle", false);
                animator.SetBool("AIRunning", false);
                animator.SetBool("AICutting", true);
            }
        }

        public void TurnOnBools()
        {
            cutting = true;
            reached = true;
            running = false;
            navMeshAgent.isStopped = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                navMeshAgent.isStopped = true;
                Debug.Log(other.gameObject.name);
                //rb.isKinematic = false;
                cutting = true;
                reached = true;
                running = false;
                //animator.SetBool("AIRunning", false);
                Debug.Log("NAV Trigger= "+navMeshAgent.isStopped);
                CuttingTree();
            }
            //Debug.Log("yyy " + other.gameObject.name);
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                cutting = false;
                running = true;
                animator.SetBool("AICutting", false);
            }
        }



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
