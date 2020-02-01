using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockBehaviour : MonoBehaviour
{
    private GameObject leftSock;
    private GameObject rightSock;

    private float leftTrigger;
    private float rightTrigger;
    private GameObject currentSock;

    public float upForce;
    public float forwardForce;

    public GameObject leftHand;
    public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        leftTrigger = Input.GetAxis("Left Trigger");
        rightTrigger = Input.GetAxis("Right Trigger");

        if (leftTrigger > 0 || rightTrigger > 0)
        {
            // Debug.Log("triggers work");
        }
        if (rightSock != null && rightTrigger >= 0)
        {
            Debug.Log("Dropping right sock");
            dropSock(rightSock);
            rightSock = null;
        }

        if (leftSock != null && leftTrigger == -1)
        {
            Debug.Log("Dropping sock");
            dropSock(leftSock);
            leftSock = null;
        }
        if (isCarryingLeft())
        {
            //Debug.Log(leftSock);
            Transform leftHandTransf = leftHand.GetComponent<Transform>();
            Transform leftSockTransf = leftSock.GetComponent<Transform>();
            leftSockTransf.position = leftHandTransf.position;
            if (Input.GetButton("Left Bumper")) {
                Debug.Log("Left Bumper!");
                GetComponent<Animator>().SetTrigger("ThrowLeft");
            }
        }
        if (isCarryingRight())
        {
            Debug.Log(leftSock);
            Transform rightHandTransf = rightHand.GetComponent<Transform>();
            Transform rightSockTransf = rightSock.GetComponent<Transform>();
            rightSockTransf.position = rightHandTransf.position;
            if (Input.GetButton("Right Bumper")) {
                GetComponent<Animator>().SetTrigger("ThrowRight");
            }
        }

    }
    void dropSock(GameObject sock)
    {
        sock.GetComponent<Transform>().parent = null;
        Rigidbody rb = sock.GetComponent<Rigidbody>();
        rb.detectCollisions = true;
        

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
                currentSock = other.gameObject;
                //pickUpLeftSock(other.gameObject);
                GetComponent<Animator>().SetTrigger("PickUpLeft");
            }
            if (rightTrigger == 1 && rightSock == null)
            {
                currentSock = other.gameObject;
                GetComponent<Animator>().SetTrigger("PickUpRight");

            }

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Sock")
        {
            currentSock = null;
        }
    }
    void throwLeftSock()
    {
        leftSock.GetComponent<Rigidbody>().detectCollisions = true;
        leftSock.GetComponent<Rigidbody>().AddForce(leftSock.transform.forward *forwardForce);
        leftSock.GetComponent<Rigidbody>().AddForce(leftSock.transform.up * upForce);
    }
    void throwRightSock()
    {
        rightSock.GetComponent<Rigidbody>().detectCollisions = true;
        rightSock.GetComponent<Rigidbody>().AddForce(rightSock.transform.forward * forwardForce);
        rightSock.GetComponent<Rigidbody>().AddForce(rightSock.transform.up * upForce);
    }
    public void pickUpLeftSock()
    {
        //Debug.Log("Ciao");
        leftSock = currentSock;
        //leftHand = GameObject.Find("EthanLeftHand");
        Transform leftHandTransf = leftHand.GetComponent<Transform>();
        Transform leftSockTransf = leftSock.GetComponent<Transform>();

        //leftSock.GetComponent<Rigidbody>().useGravity = false;
        Rigidbody rb = leftSock.GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        //rb.useGravity = false;

        //leftSock.GetComponent<Rigidbody>().isKinematic = true;
        leftSockTransf.position = leftHandTransf.position;
        leftSockTransf.parent = leftHand.GetComponent<Transform>();
    }
    public void pickUpRightSock()
    {
        rightSock = currentSock;
        Transform rightHandTransf = rightHand.GetComponent<Transform>();
        Transform rightSockTransf = rightSock.GetComponent<Transform>();

        //rightSock.GetComponent<Rigidbody>().useGravity = false;
        Rigidbody rb = rightSock.GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        //rb.useGravity = false;

        //rightSock.GetComponent<Rigidbody>().isKinematic = true;
        rightSockTransf.position = rightHandTransf.position;
        rightSockTransf.parent = rightHand.GetComponent<Transform>();
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
