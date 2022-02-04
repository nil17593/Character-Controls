using System.Collections;
using UnityEngine;


public class UnlockLand : MonoBehaviour
{
    #region Land Unlock
    [Header("Land Unlock")]
    [SerializeField] private GameObject connectingPart;
    [SerializeField] private GameObject connectingPart2;
    [SerializeField] private GameObject connectingPart3;
    [SerializeField] private GameObject land;
    [SerializeField] private GameObject land2;
    [SerializeField] private GameObject land3;
    #endregion


    public IEnumerator UnlockFirstLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart.SetActive(false);
        land.SetActive(true);
    }

    public IEnumerator UnlockSecondLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart2.SetActive(false);
        land2.SetActive(true);
    }

    public IEnumerator UnlockThirdLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart3.SetActive(false);
        land3.SetActive(true);
    }
}