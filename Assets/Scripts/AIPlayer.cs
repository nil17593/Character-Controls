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
            transform.position = navMeshAgent.nextPosition;
            navMeshAgent.SetDestination(targets[i].transform.position);            
        }

        private void Update()
        {
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
            if(i<4)
            i += 1;
            treeController.isNULL = false;
            reached = false;
            running = true;
            cutting = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(targets[i].transform.position);
            animator.SetBool("AIRunning", true);
        }

        public void CuttingTree()
        {
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
                cutting = true;
                reached = true;
                running = false;
                transform.LookAt(targets[i].transform.position);
                Debug.Log("NAV Trigger= " + navMeshAgent.isStopped);
                CuttingTree();
            }
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
