using System.Collections;
using UnityEngine;

namespace AI
{
    public class AIAttackRadius : MonoBehaviour
    {
        public SphereCollider sphereCollider;
        public AIPlayer AIPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Tree"))
            {
                AIPlayer.TurnOnBools();
                AIPlayer.CuttingTree();
            }
        }
    }
}