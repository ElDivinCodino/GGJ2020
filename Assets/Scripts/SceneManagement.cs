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
    public GameObject timeLabel, startingCanvas, player1, player2;
    public float timeLeft = 60;
    public int endScore = 2;

    public GameObject endScoreLabel, backGroundPanel;

    private AudioManagerFinal audio_managers;

    bool resize = false;
    public bool gameEnded = false;

    private void Start()
    {
        audio_managers = transform.GetComponent<AudioManagerFinal>();
    }

    public void Update()
    {
        if (gameEnded && Input.GetButton("Cross"))
        {
            SceneManager.LoadScene(0);
        }

        if(player1.GetComponent<scoreManager>().score >= endScore || 
            player2.GetComponent<scoreManager>().score >= endScore ||
            timeLeft <= 0)
        {
            EndGame();
        }

        timeLeft -= Time.deltaTime;
        timeLabel.GetComponent<TMPro.TextMeshProUGUI>().text = "Time Left: " + Mathf.Floor(timeLeft/60) + ":" + Mathf.RoundToInt(timeLeft % 60);
        if (Input.GetButton("Cross") && gameController.GetComponent<WMSuperRandomSpawner>().gameIsPlaying == false)
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
            audio_managers.PlayMusic();
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

    public void EndGame()
    {
        endScoreLabel.SetActive(true);
        endScoreLabel.GetComponent<TMPro.TextMeshProUGUI>().text = player1.GetComponent<scoreManager>().score + " - " + player2.GetComponent<scoreManager>().score;
        backGroundPanel.SetActive(false);
        gameEnded = true;
    }

}
