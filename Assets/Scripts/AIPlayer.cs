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
        private float timer = 15f;


        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            navMeshAgent.SetDestination(targets[i].transform.position);
        }

        void FixedUpdate()
        {
            NextTarget();
            MovingTowardsTarget();           
        }

        void MovingTowardsTarget()
        {
            if (!reached)
            {
                running = true;
                animator.SetBool("AIRunning", true);
                animator.SetBool("AIIdle", false);
                animator.SetBool("AICutting", false);
            }
        }

        void NextTarget()
        {
            if (TreeController.isNULL == true)
            {
                navMeshAgent.ResetPath();
                Debug.Log("II= " + i);
                reached = false;
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targets[i + 1].transform.position);
            }
        }

        public void CuttingTree()
        {
            //navMeshAgent.speed = 0f;
            if (cutting == true && reached==true && navMeshAgent.isStopped==true)
            {
                running = false;
                animator.SetBool("AIIdle", false);
                animator.SetBool("AIRunning", false);
                animator.SetBool("AICutting", true);
            }
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                cutting = true;
                reached = true;
                running = false;
                navMeshAgent.isStopped = true;
                CuttingTree();
            }
            Debug.Log("yyy " + other.gameObject.name);
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
