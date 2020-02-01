using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMshaker : MonoBehaviour
{
    private int timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.renderedFrameCount;
        if(timer % 10 == 0)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-20, 20), Random.Range(-5, 5), Random.Range(-20, 20)) * (float)0.3, ForceMode.Impulse);
        }
    }

}
