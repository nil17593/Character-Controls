using System.Collections;
using UnityEngine;



public class CharacterMovement : MonoBehaviour
{
    //[Tooltip("Character Settings")]
    #region Character Settings
    [Header("Character")]
    public float movementSpeed = 15f;
    public Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Rigidbody rb;
    #endregion

    //#region Obstacle
    //[Header("Tree")]
    //public GameObject Obstacle;
    //public GameObject Obstacle2;
    //public GameObject Obstacle3;
    //public GameObject Obstacle4;
    //#endregion

    //public GameObject cutArera;
    #region Land Unlock
    [Header("Land Unlock")]
    public GameObject connectingPart;
    public GameObject connectingPart2;
    public GameObject connectingPart3;
    public GameObject land;
    public GameObject land2;
    public GameObject land3;
    #endregion


    #region Private Variables and components
    private GameObject childprefab;
   
    //[HideInInspector] public int count = 0;
    private Animator animator;
    private Animator woodAnimator;
    private FixedJoystick fixedJoystick;
    #endregion

    private CameraController cameraController;
    private AxeController axeController;

    void Start()
    {
        //cutArera.transform.parent = this.transform;
        //movementSpeed = 9f;
        //axeController = GetComponent<AxeController>();
        rb = GetComponent<Rigidbody>();        
        animator = GetComponent<Animator>();
        fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }

    private void Update()
    {
        Movement();
        CoinPanelOnOff();

        

        //AnimationAttack();
    }
    

    //private void StartAttack()
    //{
    //    if(transform.LookAt())
    //}
    //private void AnimationAttack()
    //{
    //    Vector3 colliderSize = Vector3.one * 3f;
    //    Collider[] colliderArray = Physics.OverlapBox(cutArera.transform.position, colliderSize);
    //    foreach(Collider collider in colliderArray)
    //    {
    //        if(collider.TryGetComponent<TreeController>(out TreeController tree))
    //        {
    //            Attack();
    //        }
    //    }
    //}

    private void Movement()
    {
        rb.velocity = new Vector3(fixedJoystick.Horizontal * movementSpeed, rb.velocity.y, fixedJoystick.Vertical * movementSpeed);
        if (fixedJoystick.Horizontal != 0f || fixedJoystick.Vertical != 0f)
        {
            animator.SetBool("Idle", false);
            //Debug.Log("IF");
            animator.SetBool("Running", true);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
        }
    }

    public void Attack()
    {
        animator.SetBool("Idle" ,false);
        animator.SetBool("Running", false);
        animator.SetBool("Attack" , true);
    }

    public void CoinPanelOnOff()
    {
        float distance = Vector3.Distance(this.transform.position, UIManager.Instance.coinPanel.transform.position);
        if (distance < 15f)
        {
            UIManager.Instance.coinPanel.SetActive(true);
        }
        else
        {
            UIManager.Instance.coinPanel.SetActive(false);
        }

        float dist = Vector3.Distance(this.transform.position, UIManager.Instance.coinPanel2.transform.position);
        if (dist < 15f)
        {
            UIManager.Instance.coinPanel2.SetActive(true);
        }

        else
        {
            UIManager.Instance.coinPanel2.SetActive(false);
        }

        float dist2 = Vector3.Distance(this.transform.position, UIManager.Instance.coinPanel2.transform.position);
        if (dist < 17f)
        {
            UIManager.Instance.coinPanel3.SetActive(true);
        }
        else
        {
            UIManager.Instance.coinPanel3.SetActive(false);
        }
    } 


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == connectingPart && UIManager.Instance.score > 1)
        {
            StartCoroutine(UnlockLand());  
        }

        else if (collision.gameObject == connectingPart3 && UIManager.Instance.score > 2)
        {
            StartCoroutine(UnlockThirdLand());
        }

        else if(collision.gameObject == connectingPart2 && UIManager.Instance.score > 3)
        {
            StartCoroutine(UnlockSecondLand());
        }

        //if (count >= 1)
        //{
        //    if (collision.gameObject == woodsellArea)
        //    {
        //        StartCoroutine(UnloadWoods());
        //    }
        //}
        else
        {
            connectingPart.SetActive(true);
            land.SetActive(false);
        }
        
    }

    public void StopAttck()
    {
        animator.SetBool("Attack", false);

        if (fixedJoystick.Horizontal != 0f || fixedJoystick.Vertical != 0f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Tree"))
        {
            Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Attack", false);

        if (fixedJoystick.Horizontal != 0f || fixedJoystick.Vertical != 0f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
        }
    }

    IEnumerator UnlockLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart.SetActive(false);
        land.SetActive(true);
    }

    IEnumerator UnlockSecondLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart2.SetActive(false);
        land2.SetActive(true);
    }

    IEnumerator UnlockThirdLand()
    {
        yield return new WaitForSeconds(1f);
        connectingPart3.SetActive(false);
        land3.SetActive(true);
    }

    IEnumerator UnloadWoods()
    {
        yield return new WaitForSeconds(1f);
        Destroy(childprefab);
    }
 
}
 

/*void OnDisable()
    {
        handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }
*/