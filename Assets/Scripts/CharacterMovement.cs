using System.Collections;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    #region Character Settings
    [Header("Character")]
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Rigidbody rb;
    #endregion

    #region Land Unlock
    [Header("Land Unlock")]
    [SerializeField] private GameObject connectingPart;
    [SerializeField] private GameObject connectingPart2;
    [SerializeField] private GameObject connectingPart3;
    [SerializeField] private GameObject land;
    [SerializeField] private GameObject land2;
    [SerializeField] private GameObject land3;
    #endregion


    #region Private Variables and components
    private GameObject childprefab;
    private Animator animator;
    private Animator woodAnimator;
    private FixedJoystick fixedJoystick;
    #endregion

    private CameraController cameraController;
    private TreeController treeController;

    public ProgressBar progressBar;
    public bool cutting = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        animator = GetComponent<Animator>();
        fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }

    private void Update()
    {
        Movement();
        CoinPanelOnOff();
    }

    private void Movement()
    {
        rb.velocity = new Vector3(fixedJoystick.Horizontal * movementSpeed, rb.velocity.y, fixedJoystick.Vertical * movementSpeed);
        if (fixedJoystick.Horizontal != 0f || fixedJoystick.Vertical != 0f)
        {           
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Running", false);
        }
    }

    void Attack()
    {
        cutting = true;
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
        if (collision.gameObject.CompareTag("ConnectingPart") && UIManager.Instance.score > 1)
        {
            StartCoroutine(UnlockLand());
        }

        if (collision.gameObject == connectingPart3 && UIManager.Instance.score > 2)
        {
            StartCoroutine(UnlockThirdLand());
        }

        else if(collision.gameObject == connectingPart2 && UIManager.Instance.score > 3)
        {
            StartCoroutine(UnlockSecondLand());
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            cutting = true;
            Attack();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        cutting = false;
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
