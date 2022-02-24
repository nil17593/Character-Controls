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

        public static AIPlayer instance;

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            i = 0;
            //transform.position = navMeshAgent.nextPosition;
            navMeshAgent.SetDestination(targets[i].transform.position);
            //NextTarget();
            //CuttingTree();
        }

        void Update()
        {
            //float dist = Vector3.Distance(transform.position, targets[i].transform.position);
            //if (dist <= 4f)
            //{
            //    Debug.Log("DISTANCE= " + dist);
            //    navMeshAgent.isStopped = true;
            //    Debug.Log("NAV STOP= " + navMeshAgent.isStopped);
            //    reached = true;
            //    cutting = true;
            //    running = false;
            //    CuttingTree();
            //}
            //else
            //{
            //    reached = false;
            //    navMeshAgent.isStopped = false;
            //}
            
            //NextTarget();
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

        public void NextTarget()
        {
            //if (treeController.isNULL == true)
            //{
                i += 1;
                navMeshAgent.SetDestination(targets[i].transform.position);
                //MovingTowardsTarget();
                Debug.Log("II= " + i);
                reached = false;
                running = true;
                navMeshAgent.isStopped = false;
                Debug.Log("NAV= " + navMeshAgent.isStopped);
                //treeController.isNULL =false;
                //TreeController.isNULL = false;
                //i = (i + 1) % targets.Length;
                //if (targets.Length == 0)
                //    return;
                //navMeshAgent.destination = targets[i].transform.position;
                //i = (i + 1) % targets.Length;
            //}
        }

        public void CuttingTree()
        {
            //navMeshAgent.speed = 0f;
            if (cutting == true && reached==true  && running == false && navMeshAgent.isStopped == true)
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
                Debug.Log("NAV Trigger= " + navMeshAgent.isStopped);
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
