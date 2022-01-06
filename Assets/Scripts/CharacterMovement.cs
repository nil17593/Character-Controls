using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
  
    public float movementSpeed = 15f;
    public Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Rigidbody rb;
  
    private Animator animator;
    private FixedJoystick fixedJoystick;
    //private float size;
    //public Button attackButton;

    public GameObject Obstacle;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Obstacle4;

    private List<Transform> segments;
    //[SerializeField] private Transform segmentPrefab;

    public GameObject woodPrefab;
    public Transform woodInstantiateArea;
    public Transform woodInstantiateArea2;
    public Transform woodInstantiateArea3;
    public Transform woodInstantiateArea4;

    //public GameObject dummyblock;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        segments = new List<Transform>();   
    }

    private void Update()
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


    void Attack()
    {
        animator.SetBool("Idle" ,false);    
        animator.SetBool("Attack" , true);
    }

    void Run()
    {
        if (fixedJoystick.Horizontal >= 0.5f || fixedJoystick.Vertical >= 0.5f)
        {
            animator.SetBool("Walking", false);
            animator.SetFloat("Speed", 1f);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
       
    //    if (collision.gameObject.CompareTag("Tree"))
    //    {
    //        animator.SetBool("Attack", true);
    //        animator.SetBool("Running", false);
    //        //animator.SetBool("Idle", false);
    //        //animator.SetBool("Running", false);

    //        Vector3 reduceSize = new Vector3(0f, 0.1f, 0f);
    //        if (Obstacle.transform.localScale.y > 0)
    //        {
    //            Obstacle.transform.localScale -= reduceSize;
    //        }
    //    }
    //    //Debug.Log(collision.gameObject.name);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tree") && Obstacle.transform.localScale.y > 4f)
        {
            Attack();
            UIManager.Instance.value += 0.3f;
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle.transform.localScale.y > 0)
            {               
                Obstacle.transform.localScale -= reduceSize;
                if (Obstacle.transform.localScale.y < 5f)
                {
                    GameObject childprefab = Instantiate(woodPrefab, woodInstantiateArea.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea.transform.rotation;
                    //childprefab.transform.Rotation = Vector3(Quaternion.identity);
                }
            }
            if (Obstacle.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle.transform.localScale.y < 5)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree2") && Obstacle2.transform.localScale.y > 4f)
        {
            Attack();
            UIManager.Instance.value += 0.3f;
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle2.transform.localScale.y > 0)
            {
                Obstacle2.transform.localScale -= reduceSize;
                if (Obstacle2.transform.localScale.y < 5f)
                {
                    GameObject childprefab = Instantiate(woodPrefab, woodInstantiateArea2.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea2.transform.rotation;

                }
            }
            if (Obstacle2.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle2.transform.localScale.y < 5)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree3") && Obstacle3.transform.localScale.y > 4f)
        {
            Attack();
            UIManager.Instance.value += 0.3f;
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle3.transform.localScale.y > 0)
            {
                Obstacle3.transform.localScale -= reduceSize;
                if (Obstacle3.transform.localScale.y < 5f)
                {
                    GameObject childprefab = Instantiate(woodPrefab, woodInstantiateArea3.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea3.transform.rotation;

                }
            }
            if (Obstacle3.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle3.transform.localScale.y < 5)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
            UIManager.Instance.pb.BarValue += UIManager.Instance.value;
            //UIManager.Instance.progressText = UIManager.Instance.pb.BarValue;
        }

        else if (other.gameObject.CompareTag("Tree4") && Obstacle4.transform.localScale.y > 4f)
        {
            Attack();
            UIManager.Instance.value += 0.3f;
            //animator.SetBool("Idle", false);
            //animator.SetBool("Running", false);

            Vector3 reduceSize = new Vector3(0f, 1f, 0f);
            if (Obstacle4.transform.localScale.y > 0)
            {
                Obstacle4.transform.localScale -= reduceSize;
                if (Obstacle4.transform.localScale.y < 5f)
                {
                    GameObject childprefab = Instantiate(woodPrefab, woodInstantiateArea4.transform.position, Quaternion.identity) as GameObject;
                    childprefab.transform.parent = GameObject.FindGameObjectWithTag("parent object").transform;
                    childprefab.transform.rotation = woodInstantiateArea4.transform.rotation;

                }
            }
            if (Obstacle4.transform.localScale.y < 14)
            {
                UIManager.Instance.IncreaseScore(1);
            }
            if (Obstacle4.transform.localScale.y < 5)
            {
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


    //void StoreWood()
    //{
    //    for(int i=)
    //}
}




/*void OnDisable()
    {
        handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }
*/