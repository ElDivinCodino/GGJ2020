using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class SceneManagement : MonoBehaviour
{
    public float resizeSpeed;
    public GameObject gameUI;

    bool resize = false;

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GetComponent<Animator>().SetTrigger("Pressed");
            resize = true;
            gameUI.SetActive(true);
        }

        if(resize && transform.parent.localScale.x > 0)
        {
            Vector3 scale = transform.parent.localScale - (Vector3.one * Time.deltaTime * resizeSpeed);
            transform.parent.localScale = scale;
        }
        else if (resize)
        {
            resize = false;
        }
    }
}
