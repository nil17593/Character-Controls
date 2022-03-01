using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIAttckRadius : MonoBehaviour
    {
        public BoxCollider Collider;

        private void Start()
        {
            Collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<TreeController>() != null)
            {
                AIPlayer.instance.TurnOnBools();
                AIPlayer.instance.CuttingTree();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<TreeController>() != null)
            {
                AIPlayer.instance.TurnOffOnTriggerExit();
            }
        }
    }
}