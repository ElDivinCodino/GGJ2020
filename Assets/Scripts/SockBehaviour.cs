using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SockBehaviour : MonoBehaviour
{
    private GameObject leftSock;
    private GameObject rightSock;

    private float leftTrigger;
    private float rightTrigger;
    //private GameObject currentSock;
    private bool leftBumperPressed;
    private bool rightBumperPressed;

    public string leftBumperInput = "Left Bumper";
    public string rightBumperInput = "Right Bumper";
    public string leftTriggerInput = "Left Trigger";
    public string rightTriggerInput = "Right Trigger";


    public float upForce;
    public float forwardForce;

    public GameObject leftHand;
    public GameObject rightHand;




    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (leftTriggerInput == "Left Trigger" && rightTriggerInput == "Right Trigger")
        {
            leftTrigger = Input.GetAxis(leftTriggerInput);
            rightTrigger = Input.GetAxis(rightTriggerInput);
        }
        else
        {
            leftTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.J));
            rightTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.K));
            if(leftTrigger == 0)
            {
                leftTrigger = -1;
            }
            if (rightTrigger == 0)
            {
                rightTrigger = -1;
            }
        }

        //if (leftTrigger > 0 || rightTrigger > 0)
        //{
        //    // Debug.Log("triggers work");
        //}

        if (rightSock != null && rightTrigger == -1)
        {
            Debug.Log("rightSock " + rightSock);
            Debug.Log("rightTrigger " + rightTrigger);
            Debug.Log("Dropping right sock");
            dropSock(rightSock);
            rightSock = null;
        }

        if (leftSock != null && leftTrigger == -1)
        {
            Debug.Log("leftSock " + leftSock);
            Debug.Log("leftTrigger " + leftTrigger);
            Debug.Log("Dropping sock");
            dropSock(leftSock);
            leftSock = null;
        }
        if (isCarryingLeft())
        {
            //Debug.Log(leftSock);
            if(leftBumperInput == "Left Bumper")
            {
                if (Input.GetButton(leftBumperInput) && !leftBumperPressed)
                {
                    leftBumperPressed = true;
                    Debug.Log("Left Bumper!");
                    GetComponent<Animator>().SetTrigger("ThrowLeft");

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.H) && !leftBumperPressed)
                {
                    leftBumperPressed = true;
                    Debug.Log("Left Bumper!");
                    GetComponent<Animator>().SetTrigger("ThrowLeft");

                }
            }
        }
        if (isCarryingRight())
        {
            //Debug.Log(rightSock);
            if (rightBumperInput == "Right Bumper")
            {
                if (Input.GetButton(rightBumperInput) && !rightBumperPressed)
                {
                    rightBumperPressed = true;
                    Debug.Log("Right Bumper!");
                    GetComponent<Animator>().SetTrigger("ThrowRight");

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.L) && !rightBumperPressed)
                {
                    rightBumperPressed = true;
                    Debug.Log("Right Bumper!");
                    GetComponent<Animator>().SetTrigger("ThrowRight");

                }
            }
        }

    }
    void dropSock(GameObject sock)
    {
        //Vector3 sockPosition = sock.transform.position;
        sock.GetComponent<Transform>().parent = null;
        Rigidbody rb = sock.GetComponent<Rigidbody>();
        //rb.detectCollisions = true;
        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.isKinematic = false;
        //sock.transform.position = sockPosition;
        sock.GetComponent<BoxCollider>().enabled = true;


    }

    private void OnCollisionEnter(Collision other)
    {
        if(leftTriggerInput == "Left Trigger" && rightTriggerInput == "Right Trigger")
        {
            leftTrigger = Input.GetAxis(leftTriggerInput);
            rightTrigger = Input.GetAxis(rightTriggerInput);
        }
        else
        {
            leftTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.J));
            rightTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.K));
        }
        if (other.gameObject.tag == "Sock")
        {
            // Debug.Log("collider works");
            // Debug.Log("Left Trigger: " + leftTrigger);
            if (leftTrigger == 1 && !isCarryingLeft())
            {
                leftSock = other.gameObject;
                GetComponent<Animator>().SetTrigger("PickUpLeft");
            }
            if (rightTrigger == 1 && !isCarryingRight())
            {
                rightSock = other.gameObject;
                GetComponent<Animator>().SetTrigger("PickUpRight");

            }

        }
    }

    void throwLeftSock()
    {
        throwSock(leftSock, "CarryingLeft");
    }
    void throwRightSock()
    {
        throwSock(rightSock, "CarryingRight");
    }
    void throwSock(GameObject sock, string carrying)
    {
        Debug.Log(forwardForce);
        Debug.Log(upForce);
        sock.GetComponent<Transform>().parent = null;
        Rigidbody rb = sock.GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        rb.AddForce(transform.up * upForce, ForceMode.Impulse);
        rb.useGravity = true;

        sock.GetComponent<BoxCollider>().enabled = true;
        if (carrying == "CarryingLeft")
        {
            leftBumperPressed = false;
        }
        else
        {
            rightBumperPressed = false;
        }
        GetComponent<Animator>().SetBool(carrying, false);
    }
    public void pickUpLeftSock()
    {
        Debug.Log("Picking Up left");
        pickUpSock(leftSock.transform, leftHand.transform, "CarryingLeft");
    }
    public void pickUpRightSock()
    {
        Debug.Log("Picking Up right");
        pickUpSock(rightSock.transform, rightHand.transform, "CarryingRight");
    }
    void pickUpSock(Transform sock, Transform hand, string carrying)
    {
        //sock.gameObject.SetActive(false);
        Rigidbody rb = sock.gameObject.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        //sock.position = hand.position;
        sock.gameObject.GetComponent<BoxCollider>().enabled = false;


        sock.parent = hand;
        sock.localPosition = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        if (carrying == "CarryingLeft")
            rotation = new Vector3(-180, 0, 0);
        sock.localRotation = Quaternion.Euler(rotation);
        rb.velocity = Vector3.zero;

        GetComponent<Animator>().SetBool(carrying, true);
    }

    public bool isCarryingLeft()
    {
        if (leftSock != null)
        {
            return true;
        }
        return false;
    }
    public bool isCarryingRight()
    {
        if (rightSock != null)
        {
            return true;
        }
        return false;
    }
}