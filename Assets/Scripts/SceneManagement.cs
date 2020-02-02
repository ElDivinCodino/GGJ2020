using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public float resizeSpeed;
    public GameObject gameUI, gameController, playButton;
    public GameObject timeLabel, startingCanvas;
    public float timeLeft = 60;

    bool resize = false;

    public void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLabel.GetComponent<TMPro.TextMeshProUGUI>().text = "Time Left: " + Mathf.Floor(timeLeft/60) + ":" + Mathf.RoundToInt(timeLeft % 60);
        if (Input.GetButton("Fire1") && gameController.GetComponent<WMSuperRandomSpawner>().gameIsPlaying == false)
        {
            GameObject[] socks = GameObject.FindGameObjectsWithTag("Sock");
            foreach(GameObject sock in socks)
            {
                sock.GetComponent<WMdestroyer>().Destruction();
            }
            gameController.GetComponent<WMSuperRandomSpawner>().meanGaussian = 400;
            gameController.GetComponent<WMSuperRandomSpawner>().max_socks = 10;
            playButton.GetComponent<Animator>().SetTrigger("Pressed");
            resize = true;
            gameUI.SetActive(true);
            gameController.GetComponent<WMSuperRandomSpawner>().gameIsPlaying = true;
        }

        if(resize && playButton.transform.parent.localScale.x > 0)
        {
            Vector3 scale = playButton.transform.parent.localScale - (Vector3.one * Time.deltaTime * resizeSpeed);
            playButton.transform.parent.localScale = scale;
        }
        else if (resize)
        {
            resize = false;
        }

    }
}
