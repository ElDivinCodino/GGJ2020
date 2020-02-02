using System;
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

    public Animator starsAnim;


    private AudioClip pickup_sound,shot_sound,colpito;

    public GameObject powerupUI;

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
        //Debug.Log("Player: " + player);
        //Debug.Log(leftBumperInput);
        //Debug.Log(rightBumperInput);
        //Debug.Log(leftTriggerInput);
        //Debug.Log(rightTriggerInput);

        pickup_sound = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManagerFinal>().pickUpSound;
        shot_sound = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManagerFinal>().player_shot;
        colpito = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManagerFinal>().player_hit;

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
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    lSockImg.GetComponent<RawImage>().color = Color.white;
                    break;
                case "Sock_black(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    lSockImg.GetComponent<RawImage>().color = Color.black;
                    break;
                case "Sock_pink(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    Color pink = new Color(252f / 255f, 15f / 255f, 192f / 255f, 1f);
                    lSockImg.GetComponent<RawImage>().color = pink;
                    break;
                case "Sock_green(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    lSockImg.GetComponent<RawImage>().color = Color.green;
                    break;
                case "Sock_yellow(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().moreThrow;
                    lSockImg.GetComponent<RawImage>().color = Color.yellow;
                    break;
                case "Sock_orange(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    Color orange = new Color(255f/255f, 117f / 255f, 20f / 255f, 1f);
                    lSockImg.GetComponent<RawImage>().color = orange;
                    break;
                case "Sock_gray(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    lSockImg.GetComponent<RawImage>().color = Color.gray;
                    break;
                case "Sock_blue(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    lSockImg.GetComponent<RawImage>().color = Color.blue;
                    break;
                case "Sock_purple(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().fast;
                    lSockImg.GetComponent<RawImage>().color = Color.magenta;
                    break;
                case "Sock_red(Clone)":
                    lSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().shield;
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
            if (lSockImg != null)
                lSockImg.GetComponent<RawImage>().color = new Color(0,0,0,0);
        }
        if (isCarryingRight())
        {
            switch (rightSock.name)
            {
                case "Sock_white(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    rSockImg.GetComponent<RawImage>().color = Color.white;
                    break;
                case "Sock_black(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    rSockImg.GetComponent<RawImage>().color = Color.black;
                    break;
                case "Sock_pink(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    Color pink = new Color(252f / 255f, 15f / 255f, 192f / 255f, 1f);
                    rSockImg.GetComponent<RawImage>().color = pink;
                    break;
                case "Sock_green(C== 1)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    rSockImg.GetComponent<RawImage>().color = Color.green;
                   break;
                case "Sock_yellow(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().moreThrow;
                    rSockImg.GetComponent<RawImage>().color = Color.yellow;
                    break;
                case "Sock_orange(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    Color orange = new Color(255f / 255f, 117f / 255f, 20f / 255f, 1f);
                    rSockImg.GetComponent<RawImage>().color = orange;
                    break;
                case "Sock_gray(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    rSockImg.GetComponent<RawImage>().color = Color.gray;
                    break;
                case "Sock_blue(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().simple;
                    rSockImg.GetComponent<RawImage>().color = Color.blue;
                    break;
                case "Sock_purple(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().fast;
                    rSockImg.GetComponent<RawImage>().color = Color.magenta;
                    break;
                case "Sock_red(Clone)":
                    rSockImg.GetComponent<RawImage>().texture = powerupUI.GetComponent<powerupUI>().shield;
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
            if (rSockImg != null)
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
        //if (Input.GetJoystickNames().Length > 0 )
        //{
        //    Debug.Log(Input.GetJoystickNames()[0]);
        //    Debug.Log(Input.GetJoystickNames()[1]);
        //}
        if (Input.GetAxis(leftTriggerInput) == 1)
        {
            Debug.Log(leftTriggerInput);
        }
        else if (Input.GetAxis(rightTriggerInput)== 1)
        {
            Debug.Log(rightTriggerInput);
        }
        else if (Input.GetButton(rightBumperInput))
        {
            Debug.Log(rightBumperInput);
        }
        else if (Input.GetButton(leftBumperInput))
        {
            Debug.Log(leftBumperInput);
        }
        else if (Input.GetButton("Cross" + player))
        {
            Debug.Log("Cross" + player);
        }
        else if (Input.GetButton("Square"+player))
        {
            Debug.Log("Square"+" "+player);
        }
        else if (Input.GetButton("Circle" + player))
        {
            Debug.Log("Circle" + player);
        }
        else if (Input.GetButton("Triangle" + player))
        {
            Debug.Log("Triangle" + player);
        }
        else  if (Input.GetButton("Jump"+player))
        {
            Debug.Log("Jump" + player);
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
        if (leftTriggerInput == "Left Trigger PC" && rightTriggerInput == "Right Trigger PC")
        {
            leftTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.J));
            rightTrigger = System.Convert.ToSingle(Input.GetKey(KeyCode.K));
            
        }
        else
        {
            leftTrigger = Input.GetAxis(leftTriggerInput);
            rightTrigger = Input.GetAxis(rightTriggerInput);
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
                if(!shielded)
                { //sockPowers (Toto')

                    GetComponent<Animator>().SetTrigger("Hit");
                    transform.GetComponent<AudioSource>().PlayOneShot(colpito);

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

            transform.GetComponent<AudioSource>().PlayOneShot(shot_sound);
            shielded = true;
        }
        else{
            Debug.Log("you cannot throw socks");
        }
    }

    public void StartStarsAnim()
    {
        starsAnim.SetBool("Hit", true);

        foreach (Transform child in starsAnim.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void StopStarsAnim()
    {
        starsAnim.SetBool("Hit", false);

        foreach (Transform child in starsAnim.gameObject.transform)
        {
            child.gameObject.SetActive(false);
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

        transform.GetComponent<AudioSource>().PlayOneShot(pickup_sound);
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