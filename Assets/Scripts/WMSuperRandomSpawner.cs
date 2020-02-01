using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMSuperRandomSpawner : MonoBehaviour {

    public GameObject[] spawnees;
    public GameObject spawnPoint;

    public int max_socks = 10;

    public float radius = 2;

    int timer = 0;

    int randomInt, randomIntTimer;
    Vector3 randomVec;
    Vector2 unitCircle;
    Vector3 unitCircle3d;

    int rand_forward;
    int rand_up;

    private int current_socks;
    private GameObject check_sock;

    GameObject throw_it;

    void Start() {
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
    }
    
    // Update is called once per frame
    void Update() {
        randomIntTimer = (int)NextGaussian(400,100);
        timer += 1;
        //check if too many socks in scene
        current_socks = GameObject.FindGameObjectsWithTag("Sock").Length;
        if(timer > randomIntTimer && current_socks < max_socks) {
            timer = 0;
            //check not to spawn two pair same colors
            do
            {
                randomInt = GetRandom(0, spawnees.Length);
                check_sock = GameObject.Find(spawnees[randomInt].name+"(Clone)");
            } while (check_sock != null);

            rand_forward = GetRandom(75, 200);
            rand_up = GetRandom(100, 400);
            spawnPoint.transform.Rotate(Vector3.up, GetRandom(0, 180));
            SpawnRandom(1);
            SpawnRandom(-1);
        }
    }

    int GetRandom(int bottomBound, int upperBound) {
        return Random.Range(bottomBound, upperBound);
    }

    Vector3 GetRandomVector (Vector3 vec) {
        unitCircle = Random.insideUnitCircle;
        unitCircle3d = new Vector3(unitCircle.x, 0, unitCircle.y);
        return (unitCircle3d * radius) + vec;
    }

    void SpawnRandom(int sign) {
        randomVec = GetRandomVector(spawnPoint.transform.position);
        throw_it = Instantiate(spawnees[randomInt], randomVec, spawnPoint.transform.rotation);
        throw_it.GetComponent<Rigidbody>().AddForce(sign * spawnPoint.transform.forward * rand_forward);
        throw_it.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.up * rand_up);
    }


    public static float NextGaussian(float mean, float standard_deviation)
        {
            return mean + NextGaussian() * standard_deviation;
        }

    public static float NextGaussian()
        {
            float v1, v2, s;
            do
            {
                v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

            return v1 * s;
        }
}