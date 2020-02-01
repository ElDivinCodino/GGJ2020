using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockBehaviour : MonoBehaviour
{
    private GameObject leftSock;
    private GameObject rightSock;

    private float leftTrigger;
    private float rightTrigger;
<<<<<<< HEAD
    //private GameObject currentSock;
    private bool leftBumperPressed;
    private bool rightBumperPressed;


    public float upForce;
    public float forwardForce;

    public GameObject leftHand;
    public GameObject rightHand;
=======
>>>>>>> master

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
=======
        
>>>>>>> master
    }

    // Update is called once per frame
    void Update()
    {
        leftTrigger = Input.GetAxis("Left Trigger");
        rightTrigger = Input.GetAxis("Right Trigger");

        //if (leftTrigger > 0 || rightTrigger > 0)
        //{
        //    // Debug.Log("triggers work");
        //}
        if (rightSock != null && rightTrigger == -1)
        {
            // drop sock
            /*
                spawn rightSock nella scena;
                rightSock = null;
            */
        }
        if (leftSock != null && leftTrigger == -1)
        {
            // drop sock
            /*
                spawn leftSock nella scena;
                leftSock = null;
            */
            Debug.Log("Dropping sock");
            leftSock.GetComponent<Transform>().parent = null;
            //leftSock.GetComponent<Rigidbody>().useGravity = true;
            Rigidbody rb = leftSock.GetComponent<Rigidbody>();
            rb.detectCollisions = true;
            //rb.useGravity = true;
            //leftSock.GetComponent<Rigidbody>().isKinematic = false;
            leftSock = null;
        }
        if (isCarryingLeft())
        {
<<<<<<< HEAD
            //Debug.Log(leftSock);
            if (Input.GetButton("Left Bumper") && !leftBumperPressed)
            {
                leftBumperPressed = true;
                Debug.Log("Left Bumper!");
                GetComponent<Animator>().SetTrigger("ThrowLeft");

            }
        }
        if (isCarryingRight())
        {
            //Debug.Log(rightSock);
            if (Input.GetButton("Right Bumper") && !rightBumperPressed)
            {
                rightBumperPressed = true;
                Debug.Log("Right Bumper!");
                GetComponent<Animator>().SetTrigger("ThrowRight");
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

=======
            Debug.Log(leftSock);
            GameObject leftHand = GameObject.Find("EthanLeftHand");
            Transform leftHandTransf = leftHand.GetComponent<Transform>();
            Transform leftSockTransf = leftSock.GetComponent<Transform>();
            leftSockTransf.position = leftHandTransf.position;
            //Debug.Log("Restting position");

        }
>>>>>>> master

    }

    private void OnCollisionEnter(Collision other)
    {
        leftTrigger = Input.GetAxis("Left Trigger");
        rightTrigger = Input.GetAxis("Right Trigger");
        if (other.gameObject.tag == "Sock")
        {
            // Debug.Log("collider works");
            // Debug.Log("Left Trigger: " + leftTrigger);
            if (leftTrigger == 1 && !isCarryingLeft())
            {
<<<<<<< HEAD
                leftSock = other.gameObject;
                GetComponent<Animator>().SetTrigger("PickUpLeft");
=======
                pickUpLeftSock(other.gameObject);
>>>>>>> master
            }
            if (rightTrigger == 1 && !isCarryingRight())
            {
                rightSock = other.gameObject;
<<<<<<< HEAD
                GetComponent<Animator>().SetTrigger("PickUpRight");

=======
>>>>>>> master
            }

        }
    }
<<<<<<< HEAD
    
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
=======

    void pickUpLeftSock(GameObject other)
    {
        //if (leftSock == null)
        //{
        //    Debug.Log("Picking up socks");
        //    // do animation only here
        //}
        leftSock = other.gameObject;
        GameObject leftHand = GameObject.Find("EthanLeftHand");
        Transform leftHandTransf = leftHand.GetComponent<Transform>();
        Transform leftSockTransf = leftSock.GetComponent<Transform>();

        //leftSock.GetComponent<Rigidbody>().useGravity = false;
        Rigidbody rb = leftSock.GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        //rb.useGravity = false;
>>>>>>> master

        sock.GetComponent<BoxCollider>().enabled = true;
        if (carrying== "CarryingLeft")
        {
            leftBumperPressed = false;
        } else
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
<<<<<<< HEAD
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

=======
>>>>>>> master
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
