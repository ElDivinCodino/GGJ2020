﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.ThirdPerson; //sockPowers (Toto')

public class SockBehaviour : MonoBehaviour
{
    public GameObject leftSock;
    public GameObject rightSock;

    private float leftTrigger;
    private float rightTrigger;
    //private GameObject currentSock;
    private bool leftBumperPressed;
    private bool rightBumperPressed;

    public string leftBumperInput = "Left Bumper";
    public string rightBumperInput = "Right Bumper";
    public string leftTriggerInput = "Left Trigger";
    public string rightTriggerInput = "Right Trigger";

    public string player = "";
    public float upForce;
    public float forwardForce;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject lSockImg;
    public GameObject rSockImg;

    //sockPowers (Toto') ------------------------------------------------------------------
    float malusDuration=3.0f;
    private IEnumerator coroutine;

    String sockSpeed="Sock_purple(Clone)"; //speed boost/zeroing
    float peakSpeed=5f;

    String sockLimiter="Sock_red(Clone)"; //only loose one/cannot pick socks
    bool canPick=true, shielded=false;

    String sockRange="Sock_green(Clone)"; //range extended/cannot trow
    float strength=1f;
    bool canThrow=true;

    /*Sock_orange--> doppio salto
    ?? --> dash (e malus dell'altro slowdown)
    ?? --> increase washing machine pool
    other?
    */

    //coroutines
    IEnumerator slowDown()
    {
        yield return new WaitForSeconds(malusDuration);
        GetComponent<ThirdPersonCharacter>().setSpeed(1f);
        Debug.Log("speed restored");
    }
    IEnumerator GOSH() //easter egg
    {
        yield return new WaitForSeconds(5f);
        GetComponent<ThirdPersonCharacter>().setSpeed(10f);
        Debug.Log("GOSH");
    }
    IEnumerator noSocks()
    {
        yield return new WaitForSeconds(malusDuration);
        canPick=true;
        Debug.Log("can now pick socks");
    }
    IEnumerator noThrow()
    {
        yield return new WaitForSeconds(malusDuration);
        canThrow=true;
        Debug.Log("can now throw socks");
    }
    // -----------------------------------------------------------------------------------

    private void Start()
    {
        leftBumperInput += player;
        rightBumperInput += player;
        leftTriggerInput += player;
        rightTriggerInput += player;
    }

    void Update()
    {
        leftTrigger = Input.GetAxis(leftTriggerInput);
        rightTrigger = Input.GetAxis(rightTriggerInput);

        if (isCarryingLeft())
        {
            switch (leftSock.name)
            {
                case "Sock_white(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.white;
                    break;
                case "Sock_black(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.black;
                    break;
                case "Sock_pink(Clone)":
                    rSockImg.GetComponent<RawImage>().color = new Color(252f, 15f, 192f);
                    break;
                case "Sock_green(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.green;
                    break;
                case "Sock_yellow(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.yellow;
                    break;
                case "Sock_orange(Clone)":
                    rSockImg.GetComponent<RawImage>().color = new Color(255f, 117f, 20f);
                    break;
                case "Sock_gray(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.gray;
                    break;
                case "Sock_blue(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.blue;
                    break;
                case "Sock_purple(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.magenta;
                    break;
                case "Sock_red(Clone)":
                    lSockImg.GetComponent<RawImage>().color = Color.red;
                    break;
            }
            if (Input.GetButton(leftBumperInput) && !leftBumperPressed)
            {
                leftBumperPressed = true;
                Debug.Log("Left Bumper!");
                GetComponent<Animator>().SetTrigger("ThrowLeft");

            }
        }
        else
        {
            lSockImg.GetComponent<RawImage>().color = new Color(0,0,0,0);
        }
        if (isCarryingRight())
        {
            switch (rightSock.name)
            {
                case "Sock_white(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.white;
                    break;
                case "Sock_black(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.black;
                    break;
                case "Sock_pink(Clone)":
                    rSockImg.GetComponent<RawImage>().color = new Color(252f, 15f, 192f);
                    break;
                case "Sock_green(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.green;
                   break;
                case "Sock_yellow(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.yellow;
                    break;
                case "Sock_orange(Clone)":
                    rSockImg.GetComponent<RawImage>().color = new Color(255f, 117f, 20f);
                    break;
                case "Sock_gray(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.gray;
                    break;
                case "Sock_blue(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.blue;
                    break;
                case "Sock_purple(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.magenta;
                    break;
                case "Sock_red(Clone)":
                    rSockImg.GetComponent<RawImage>().color = Color.red;
                    break;
            }
            if (Input.GetButton(rightBumperInput) && !rightBumperPressed)
            {
                rightBumperPressed = true;
                Debug.Log("Right Bumper!");
                GetComponent<Animator>().SetTrigger("ThrowRight");

            }
        }
        else
        {
            rSockImg.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        }

       

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
       
        

        testGamePad();
    }


    /* ----------------- */
    void testGamePad()
    {
        if (Input.GetJoystickNames().Length > 0 )
        {
            Debug.Log(Input.GetJoystickNames()[0]);
            Debug.Log(Input.GetJoystickNames()[1]);
        }
        if (Input.GetAxis("Left Trigger") == 1)
        {
            Debug.Log("Left Trigger");
        }
        else if (Input.GetAxis("Right Trigger") == 1)
        {
            Debug.Log("Right Trigger");
        }
        else if (Input.GetButton("Right Bumper"))
        {
            Debug.Log("Right Bumper");
        }
        else if (Input.GetButton("Left Bumper"))
        {
            Debug.Log("Left Bumper");
        }
        else if (Input.GetButton("Cross"))
        {
            Debug.Log("Cross");
        }
        else if (Input.GetButton("Square"+player))
        {
            Debug.Log("Square"+" "+player);
        }
        else if (Input.GetButton("Circle"))
        {
            Debug.Log("Circle");
        }
        else if (Input.GetButton("Triangle"))
        {
            Debug.Log("Triangle");
        }
    }
    /* ----------------- */



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
        
        //sockPowers (Toto')
        Debug.Log("Dropped : "+sock.name);
        if (System.String.Equals(sock.name, sockSpeed)){
            Debug.Log("speed restored");
            GetComponent<ThirdPersonCharacter>().setSpeed(1f);
        }
        if (System.String.Equals(sock.name, sockLimiter)){
            Debug.Log("shield disabled");
            shielded=false;
        }
        if (System.String.Equals(sock.name, sockRange)){
            Debug.Log("ra(n)ge restored");
            strength=1f;
        }
    }

    private void OnCollisionStay(Collision other)
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
            // If grounded
            if(other.gameObject.transform.position.y < 0)
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
            else if(other.gameObject.transform.position.y > 0.5)
            {
                if(!shielded){ //sockPowers (Toto')
                    if (leftSock != null && isCarryingLeft() && other.gameObject != leftSock && other.gameObject != rightSock)
                    {
                        dropSock(leftSock);
                    }
                    if (rightSock != null && isCarryingRight() && other.gameObject != leftSock && other.gameObject != rightSock)
                    {
                        dropSock(rightSock);
                    }
                }else{ //lazy implementation
                    if (rightSock != null && isCarryingRight() && other.gameObject != leftSock && other.gameObject != rightSock)
                    {
                        dropSock(rightSock);
                    }
                }
                //sockPowers (Toto')
                 //to avoid colliding with a dropped sock
                    Debug.Log("Hit by : "+other.gameObject.name);
                    if (System.String.Equals(other.gameObject.name, sockSpeed)){
                        Debug.Log("speed zeroed");
                        GetComponent<ThirdPersonCharacter>().setSpeed(0f);
                        coroutine = slowDown();
                        StartCoroutine(coroutine);
                    }
                    if (System.String.Equals(other.gameObject.name, sockLimiter)){
                        Debug.Log("cannot pick socks");
                        canPick=false;
                        coroutine = noSocks();
                        StartCoroutine(coroutine);
                    }
                    if (System.String.Equals(other.gameObject.name, sockRange)){
                        Debug.Log("cannot throw socks");
                        canThrow=false;
                        coroutine = noThrow();
                        StartCoroutine(coroutine);
                    }
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
        if(canThrow){
            Debug.Log(forwardForce);
            Debug.Log(upForce);
            sock.GetComponent<Transform>().parent = null;
            Rigidbody rb = sock.GetComponent<Rigidbody>();
            rb.freezeRotation = false;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * (forwardForce*strength), ForceMode.Impulse);
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
        }else{
            Debug.Log("you cannot throw socks");
        }
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
        
        //sockPowers (Toto')
        Debug.Log("Picked : "+sock.name);
        if (System.String.Equals(sock.name, sockSpeed)){
            Debug.Log("speed increase");
            GetComponent<ThirdPersonCharacter>().setSpeed(peakSpeed);
            
                /*coroutine = GOSH(); //easter egg enabler
                StartCoroutine(coroutine);*/
        }
        if (System.String.Equals(sock.name, sockLimiter)){
            Debug.Log("shield enabled");
            shielded=true;
        }
        if (System.String.Equals(sock.name, sockRange)){
            Debug.Log("ra(n)ge extended");
            strength=2f;
        }
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