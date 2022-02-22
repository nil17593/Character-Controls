using System.Collections;
using UnityEngine;

namespace AI
{
    public class AIAxeController : MonoBehaviour
    {
        #region refereance of other scripts
        private TreeController treeController;
        #endregion


        //reduce tree size while trigger with tree
        private void OnTriggerEnter(Collider other)
        {
            TreeController tree = other.transform.GetComponent<TreeController>();
            if (tree != null)
            {
                tree.ReduceSize();
                //AIWoodCollecter.Instance.WoodCollection();
            }
        }
    }
}