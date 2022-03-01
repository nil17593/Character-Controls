using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class NormalAIPlayer : MonoBehaviour
    {
        [Header("AI Player Components")]
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        private TreeController treeController;
        private Rigidbody Rigidbody;
        private Animator animator;
        public bool cutting = false;
        public bool running;
        //public float maxX, maxZ, minX, minZ;
        private BoxCollider ground;
        [SerializeField]
        private TreeController[] targets;
        [SerializeField]
        private AgentType agentType;
        [SerializeField]
        private GameObject woodSell;
        public int i = 6;
        public bool reached = false;

        public static NormalAIPlayer instance;


        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            i = 0;
            //targets = FindObjectsOfType<TreeController>();
            navMeshAgent.SetDestination(targets[i].transform.position);
            //if (AIWoodCollecter.woods.Count == 14)
            //{
            //    navMeshAgent.SetDestination(woodSell.transform.position);
            //}
        }

        private void Update()
        {
            //if (AIWoodCollecter.woods.Count >= 10)
            //{
            //    navMeshAgent.SetDestination(woodSell.transform.position);
            //}
            MovingTowardsTarget();
        }

        void MovingTowardsTarget()
        {
            if (!reached)
            {
                running = true;
                cutting = false;
                navMeshAgent.isStopped = false;
                animator.SetBool("NRun", true);
                animator.SetBool("NIdle", false);
                animator.SetBool("NCut", false);
            }
        }

        public void NextTargetOfNormalAI()
        {
            navMeshAgent.ResetPath();
            if (i < 5)
                i += 1;
            //treeController.isNULL = false;
            reached = false;
            running = true;
            cutting = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.acceleration = 50f;
            navMeshAgent.SetDestination(targets[i].transform.position);
            animator.SetBool("NRun", true);
        }

        public void CuttingTree()
        {
            if (cutting == true && reached == true && running == false && navMeshAgent.isStopped == true)
            {
                animator.SetBool("NIdle", false);
                animator.SetBool("NRun", false);
                animator.SetBool("NCut", true);
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
                //navMeshAgent.velocity=Vector3.zero;
                Debug.Log(other.gameObject.name);
                cutting = true;
                reached = true;
                running = false;
                transform.LookAt(targets[i].transform.position);
                Debug.Log("NAV Trigger= " + navMeshAgent.isStopped);
                CuttingTree();
            }
        }

        IEnumerator DestroyTargetTree()
        {
            yield return new WaitForSeconds(15f);
            TreeController.instance.DestroyTree(gameObject.GetComponent<TreeController>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                cutting = false;
                running = true;
                animator.SetBool("NCut", false);
            }
        }

        public void TurnOffOnTriggerExit()
        {
            cutting = false;
            running = true;
            animator.SetBool("NCut", false);
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