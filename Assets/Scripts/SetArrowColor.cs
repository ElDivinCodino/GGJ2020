using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArrowColor : MonoBehaviour
{
    void Start()
    {

        if (transform.parent)
        {
            Material mat = transform.parent.GetComponentInChildren<MeshRenderer>().materials[0];

            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = mat;
                child.GetComponent<Renderer>().enabled = false;
            }

            StartCoroutine(Appear());
        }
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(3);

        if (Vector3.Angle(transform.up, Vector3.up) < 90)
            transform.RotateAround(transform.parent.position, Vector3.right, 180);

        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = true;
        }
    }
}