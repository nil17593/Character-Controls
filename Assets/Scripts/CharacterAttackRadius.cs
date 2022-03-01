using System.Collections;
using UnityEngine;

namespace AI
{
    public class CharacterAttackRadius : MonoBehaviour
    {
        public BoxCollider boxCollider;       

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<TreeController>() != null)
            {
                CharacterMovement.instance.StartCutting();
                CharacterMovement.instance.LookTarget(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<TreeController>() != null)
            {
                CharacterMovement.instance.StopCutting();              
            }
        }
    }
}