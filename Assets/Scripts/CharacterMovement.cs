using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    private List<Transform> woodPrefabs;
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

    private Vector3 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        woodPrefabs = new List<Transform>();   
    }

    private void Update()
    {
        Debug.Log(count);
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
        //if(Vector3.Distance(this.transform,UIManager.Instance.coinPanel)>)
    }


    void Attack()
    {
        animator.SetBool("Idle" ,false);    
        animator.SetBool("Attack" , true);
        //transform.DOShakePosition(1f,2f,10,90,false,true);
    }

    void Run()
    {
        if (fixedJoystick.Horizontal >= 0.5f || fixedJoystick.Vertical >= 0.5f)
        {
            animator.SetBool("Walking", false);
            animator.SetFloat("Speed", 1f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision enter");
        if (collision.gameObject == connectingPart && count > 1)
        {
            StartCoroutine(UnlockLand());
            //connectingPart.SetActive(false);
        }

        else if (collision.gameObject == connectingPart3 && count > 2)
        {
            StartCoroutine(UnlockThirdLand());
            //connectingPart.SetActive(false);
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
        Debug.Log("Trigger enter");

        if (other.gameObject.CompareTag("Tree") && Obstacle.transform.localScale.y > 3f)
        {
            Attack();
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 0.5f, 0f);
            if (Obstacle.transform.localScale.y > 0)
            {               
                Obstacle.transform.localScale -= reduceSize;
                if (Obstacle.transform.localScale.y < 4f)
                {
                    childprefab = Instantiate(woodPrefab, woodInstantiateArea.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = this.transform;  // GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea.transform.rotation;
                    count += 1;
                    //childprefab.transform.Rotation = Vector3(Quaternion.identity);
                }
            }
            if (Obstacle.transform.localScale.y < 6)
            {
                UIManager.Instance.IncreaseScore(0.1f);
            }
            if (Obstacle.transform.localScale.y < 4)
            {
                UIManager.Instance.value += 0.5f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree2") && Obstacle2.transform.localScale.y > 4f)
        {
            Attack();
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle2.transform.localScale.y > 0)
            {
                Obstacle2.transform.localScale -= reduceSize;
                if (Obstacle2.transform.localScale.y < 5f)
                {
                    childprefab = Instantiate(woodPrefab, woodInstantiateArea2.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = this.transform; // GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea2.transform.rotation;
                    count += 1;
                }
            }
            if (Obstacle2.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(0.1f);
            }
            if (Obstacle2.transform.localScale.y < 5)
            {
                UIManager.Instance.value += 0.5f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree3") && Obstacle3.transform.localScale.y > 4f)
        {
            Attack();
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle3.transform.localScale.y > 0)
            {
                Obstacle3.transform.localScale -= reduceSize;
                if (Obstacle3.transform.localScale.y < 5f)
                {
                    childprefab = Instantiate(woodPrefab, woodInstantiateArea3.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = this.transform; // GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea3.transform.rotation;
                    count += 1;
                }
            }
            if (Obstacle3.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(0.1f);
            }
            if (Obstacle3.transform.localScale.y < 5)
            {
                UIManager.Instance.value += 0.5f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree4") && Obstacle4.transform.localScale.y > 4f)
        {
            Attack();
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle4.transform.localScale.y > 0)
            {
                Obstacle4.transform.localScale -= reduceSize;
                if (Obstacle4.transform.localScale.y < 5f)
                {
                    childprefab = Instantiate(woodPrefab, woodInstantiateArea4.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = this.transform;// GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea4.transform.rotation;
                    count += 1;
                }
            }
            if (Obstacle4.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(0.1f);
            }
            if (Obstacle4.transform.localScale.y < 5)
            {
                UIManager.Instance.value += 0.5f;
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
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
        //else
        //{
        //    //animator.SetBool("Idle", true);
        //    //animator.SetBool("Running", false);
        //}
        //animator.SetBool("Idle", true);
        //animator.SetBool("Attack", false);
    }
    
    IEnumerator UnlockLand()
    {
        yield return new WaitForSeconds(2f);
        connectingPart.SetActive(false);
        land.SetActive(true);
        //MeshRenderer.Instantiate(land, land.transform.position,Quaternion.identity);
    }

    IEnumerator UnlockSecondLand()
    {
        yield return new WaitForSeconds(2f);
        connectingPart2.SetActive(false);
        land2.SetActive(true);
    }

    IEnumerator UnlockThirdLand()
    {
        yield return new WaitForSeconds(2f);
        connectingPart3.SetActive(false);
        land3.SetActive(true);
    }

    IEnumerator UnloadWoods()
    {
        yield return new WaitForSeconds(5f);
        Destroy(childprefab);
        //woodPrefab.transform.Do(4f, 3f, 5f);
        
        //woodPrefab.transform.position = new Vector3(woodsellArea.transform.position.x, woodsellArea.transform.position.y, woodsellArea.transform.position.z);

        //childprefab.transform.parent = woodsellArea.transform;
    }

 
}
 

/*void OnDisable()
    {
        handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }
*/