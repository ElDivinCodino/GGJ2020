using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.ThirdPerson;

public class scoreManager : MonoBehaviour
{
    private GameObject leftSock;
    private GameObject rightSock;
    public GameObject playerPoints;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        leftSock = GetComponent<SockBehaviour>().leftSock;
        rightSock = GetComponent<SockBehaviour>().rightSock;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        Debug.Log(leftSock.name);
        Debug.Log(rightSock.name);
        if (other.gameObject.tag == "bin" && leftSock != null && rightSock != null && leftSock.name == rightSock.name)
        {
            leftSock.GetComponent<WMdestroyer>().Destruction();
            rightSock.GetComponent<WMdestroyer>().Destruction();
            score += 2;
            playerPoints.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();

            GetComponent<ThirdPersonCharacter>().setSpeed(1f);
            GetComponent<SockBehaviour>().shielded = false;
            GetComponent<SockBehaviour>().strength = 1f;
        }
    }
}
