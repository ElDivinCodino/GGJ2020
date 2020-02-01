using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class SceneManagement : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GetComponent<Animator>().SetTrigger("Pressed");
            transform.parent.gameObject.SetActive(false);
        }
    }
}
