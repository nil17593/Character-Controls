using System.Collections;
using UnityEngine;
using DG.Tweening;


public class DoTweenController : MonoBehaviour
{
    [SerializeField] private Vector3 targetLocation;
    [Range(1.0f,10.0f), SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private Ease moveEase = Ease.Linear;
    [SerializeField] private DoTweenType doTweenType = DoTweenType.MoveOneWay;

    private enum DoTweenType
    {
        MoveOneWay
    }
    
    void Start()
    {
        targetLocation = Vector3.zero;
        if (doTweenType == DoTweenType.MoveOneWay)
        {
            if (targetLocation == Vector3.zero)
            {
                targetLocation = transform.position;
            }
            else
            {
                transform.DOMove(targetLocation, moveDuration);
            }
        }
    }

    
    void Update()
    {

    }
}