using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMSuperRandomSpawner : MonoBehaviour {

	public GameObject[] spawnees;
    public GameObject spawnPoint;

    public float radius = 2;

    int randomInt;
    Vector3 randomVec;
    Vector2 unitCircle;
    Vector3 unitCircle3d;
    int rand_forward;
    int rand_up;

    GameObject obj;

    void Start() {
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
    }
    
    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            randomInt = GetRandom(0,spawnees.Length);
            rand_forward = GetRandom(75, 200);
            rand_up = GetRandom(100, 400);
            //spawnPoint.transform.RotateAround(Vector3.up, GetRandom(0, 180));
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
        obj = Instantiate(spawnees[randomInt], randomVec, spawnPoint.transform.rotation);
        obj.GetComponent<Rigidbody>().AddForce(sign * spawnPoint.transform.forward * rand_forward);
        obj.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.up * rand_up);
    }
}