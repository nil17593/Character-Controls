using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CharacterMovement : MonoBehaviour
{
    //[Tooltip("Character Settings")]
    #region Character Settings
    [Header("Character")]
    public float movementSpeed = 15f;
    public Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Rigidbody rb;
    #endregion

    #region Obstacle
    [Header("Tree")]
    public GameObject Obstacle;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Obstacle4;
    #endregion

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
   
    [HideInInspector] public int count = 0;
    private Animator animator;
    private FixedJoystick fixedJoystick;
    #endregion


    #region Collected wood 
    [Header("Collected wood by player")]
    public GameObject woodPrefab;
    public Transform woodInstantiateArea;
    public Transform woodInstantiateArea2;
    public Transform woodInstantiateArea3;
    public Transform woodInstantiateArea4;
    public GameObject woodsellArea;
    #endregion



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
        animator.SetBool("Attack" , true);
    }

    void CoinPanelOnOff()
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
        if (collision.gameObject == connectingPart && count > 1)
        {
            StartCoroutine(UnlockLand());
        }

        else if (collision.gameObject == connectingPart3 && count > 2)
        {
            StartCoroutine(UnlockThirdLand());
        }

        else if(collision.gameObject == connectingPart2 && count > 3)
        {
            StartCoroutine(UnlockSecondLand());
        }

        if (count >= 1)
        {
            if (collision.gameObject == woodsellArea)
            {
                StartCoroutine(UnloadWoods());
            }
        }
        else
        {
            connectingPart.SetActive(true);
            land.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Tree") && Obstacle.transform.localScale.y > 3f)
        {
            Attack();

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);

            if (Obstacle.transform.localScale.y > 0f)
            {
                Obstacle.transform.localScale -= reduceSize;
                if (Obstacle.transform.localScale.y < 4f)
                {
                    childprefab = Instantiate(woodPrefab, new Vector3(Obstacle.transform.position.x, Obstacle.transform.position.y + 5f, Obstacle.transform.position.z), Quaternion.identity) as GameObject;
                    childprefab.transform.DOMove(woodInstantiateArea.transform.position, 0.4f);
                    childprefab.transform.rotation = woodInstantiateArea.transform.rotation;
                    childprefab.transform.parent = this.transform;  // GameObject.FindGameObjectWithTag("parent object").transform;

                    count += 1;
                }
            }
            if (Obstacle.transform.localScale.y < 6f)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle.transform.localScale.y < 4f)
            {
                UIManager.Instance.value += 1;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
        }

        else if (other.gameObject.CompareTag("Tree2") && Obstacle2.transform.localScale.y > 3f)
        {
            Attack();

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle2.transform.localScale.y > 0)
            {
                Obstacle2.transform.localScale -= reduceSize;
                if (Obstacle2.transform.localScale.y < 4f)
                {
                    childprefab = Instantiate(woodPrefab, new Vector3(Obstacle2.transform.position.x, Obstacle2.transform.position.y + 5f, Obstacle2.transform.position.z), Quaternion.identity) as GameObject;
                    childprefab.transform.DOMove(woodInstantiateArea2.transform.position, 0.4f);
                    childprefab.transform.rotation = woodInstantiateArea2.transform.rotation;
                    childprefab.transform.parent = this.transform;
                    count += 1;
                }
            }
            if (Obstacle2.transform.localScale.y < 6f)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle2.transform.localScale.y < 4f)
            {
                UIManager.Instance.value += 1f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
        }

        else if (other.gameObject.CompareTag("Tree3") && Obstacle3.transform.localScale.y > 3f)
        {
            Attack();

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle3.transform.localScale.y > 0)
            {
                Obstacle3.transform.localScale -= reduceSize;
                if (Obstacle3.transform.localScale.y < 4f)
                {
                    childprefab = Instantiate(woodPrefab, new Vector3(Obstacle3.transform.position.x, Obstacle3.transform.position.y + 5f, Obstacle3.transform.position.z), Quaternion.identity) as GameObject;
                    childprefab.transform.DOMove(woodInstantiateArea3.transform.position, 0.4f);
                    childprefab.transform.parent = this.transform;
                    childprefab.transform.rotation = woodInstantiateArea3.transform.rotation;
                    count += 1;
                }
            }
            if (Obstacle3.transform.localScale.y < 6f)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle3.transform.localScale.y < 4f)
            {
                UIManager.Instance.value += 1f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
        }

        else if (other.gameObject.CompareTag("Tree4") && Obstacle4.transform.localScale.y > 3f)
        {
            Attack();

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle4.transform.localScale.y > 0)
            {
                Obstacle4.transform.localScale -= reduceSize;
                if (Obstacle4.transform.localScale.y < 4f)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        childprefab = Instantiate(woodPrefab, new Vector3(Obstacle4.transform.position.x, Obstacle4.transform.position.y + 5f, Obstacle4.transform.position.z), Quaternion.identity) as GameObject;
                        childprefab.transform.DOMove(woodInstantiateArea4.transform.position, 0.1f);
                        childprefab.transform.parent = this.transform;
                        childprefab.transform.rotation = woodInstantiateArea4.transform.rotation;
                    }
                    count += 1;
                }
            }
            if (Obstacle4.transform.localScale.y < 6f)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle4.transform.localScale.y < 4f)
            {
                UIManager.Instance.value += 1f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
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