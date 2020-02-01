using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockBehaviour : MonoBehaviour
{
    private GameObject leftSock;
    private GameObject rightSock;

    private float leftTrigger;
    private float rightTrigger;

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
            Debug.Log(leftSock);
            GameObject leftHand = GameObject.Find("EthanLeftHand");
            Transform leftHandTransf = leftHand.GetComponent<Transform>();
            Transform leftSockTransf = leftSock.GetComponent<Transform>();
            leftSockTransf.position = leftHandTransf.position;
            //Debug.Log("Restting position");

        }

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
                pickUpLeftSock(other.gameObject);
            }
            if (rightTrigger == 1 && rightSock == null)
            {
                rightSock = other.gameObject;
            }

        }
    }

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

        //leftSock.GetComponent<Rigidbody>().isKinematic = true;
        leftSockTransf.position = leftHandTransf.position;
        leftSockTransf.parent = leftHand.GetComponent<Transform>();
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
