using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AgentManager : MonoBehaviour
    {

        [SerializeField]
        private NavMeshAgent[] agents;

        void Start()
        {
            agents = GameObject.FindObjectsOfType<NavMeshAgent>();

            for(int i = 0; i < agents.Length; i++)
            {
                NavMeshPath meshPath = new NavMeshPath();

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}