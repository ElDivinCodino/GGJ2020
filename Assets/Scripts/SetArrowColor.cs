using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArrowColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent)
        {
            Material mat = transform.parent.GetComponentInChildren<MeshRenderer>().materials[0];

            Debug.Log(mat.name);

            foreach (Transform child in transform)
            {
                Debug.Log(child.gameObject.name);
                child.GetComponent<Renderer>().material = mat;
            }
                //.materials[0] = mat;
        }
    }
}
