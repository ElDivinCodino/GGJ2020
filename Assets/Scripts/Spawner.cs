using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;

    public float posMin, posMax;
    public int toSpawnMin, toSpawnMax, maxElements, seconds, centralArea;
    //Vector3[] spawnvalues;

    List<GameObject> spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = new List<GameObject>();
        posMin = -4.5f;
        posMax = 4.5f;
        toSpawnMin = 5;
        toSpawnMax = 15;
        maxElements = 35;
        seconds = 1;
        centralArea = 10;

        StartCoroutine(DestroyElements(seconds));
        //InitializeEnvironment();
    }

    //Inizializzazione statica
    void InitializeEnvironment()
    {
        int j = Random.Range(toSpawnMin, toSpawnMax);
        while (j > 0)
        {
            Vector3 position = new Vector3(Mathf.Round(Random.Range(posMin, posMax)), 0, Mathf.Round(Random.Range(posMin, posMax)));
            while (spawned.Exists(cube => cube.transform.position == position) && ((position.x > -centralArea) && (position.x < centralArea) && (position.y > -centralArea) && (position.y < centralArea)))
            {
                position = new Vector3(Mathf.Round(Random.Range(posMin, posMax)), 0, Mathf.Round(Random.Range(posMin, posMax)));
            }
            GameObject created = Instantiate(cubes[Random.Range(0, cubes.Length)], position, gameObject.transform.rotation);
            spawned.Add(created);
            j--;
        }
    }

    //Versione con aggiornamento periodico
    IEnumerator DestroyElements(int seconds)
    {
        int i = 0;
        while (i < 1000000) {

            InitializeEnvironment();

            yield return new WaitForSeconds(seconds);

            int j = Random.Range(toSpawnMin, toSpawnMax);
            while ((j > 0 || spawned.Count < maxElements) && spawned.Count > 0)
            {
                GameObject toDestroy = spawned[0];
                spawned.Remove(toDestroy);
                Destroy(toDestroy);
            }
        i++;
        }
    }
}
