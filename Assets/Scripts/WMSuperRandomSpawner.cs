using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMSuperRandomSpawner : MonoBehaviour {

	public GameObject[] spawnees;
    public GameObject[] spawnPoints;

    public float radius = 2;

    int randomInt;
    int randomIntTwo;
    Vector3 randomVec;
    Vector2 unitCircle;
    Vector3 unitCircle3d;

    GameObject obj;

    void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }
    
    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            randomInt = GetRandom(spawnees.Length);
            randomIntTwo = GetRandom(spawnPoints.Length);
            SpawnRandom(1);
            SpawnRandom(-1);
        }
    }

    int GetRandom(int count) {
        return Random.Range(0, count);
    }

    Vector3 GetRandomVector (Vector3 vec) {
        unitCircle = Random.insideUnitCircle;
        unitCircle3d = new Vector3(unitCircle.x, 0, unitCircle.y);
        return (unitCircle3d * radius) + vec;
    }

    void SpawnRandom(int sign) {
        randomVec = GetRandomVector(spawnPoints[randomIntTwo].transform.position);
        obj = Instantiate(spawnees[randomInt], randomVec, spawnPoints[randomIntTwo].transform.rotation);
        obj.GetComponent<Rigidbody>().AddForce(sign * transform.forward * 1000);
    }
}