using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;

    float posMin, posMax;
    int toSpawnMin, toSpawnMax, maxElements;
    //Vector3[] spawnvalues;

    List<GameObject> spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = new List<GameObject>();
        StartCoroutine(DestroyElements());
        posMin = -4.5f;
        posMax = 4.5f;
        toSpawnMin = 5;
        toSpawnMax = 15;
        maxElements = 35;
    }

    IEnumerator DestroyElements()
    {
        int i = 0;
        while (i < 1000000) {

            int j = Random.Range(toSpawnMin, toSpawnMax);
            while (j > 0)
            {
                Vector3 position = new Vector3(Mathf.Round(Random.Range(posMin, posMax)), 0, Mathf.Round(Random.Range(posMin, posMax)));
                while (spawned.Exists(cube => cube.transform.position == position ))
                {
                    position = new Vector3(Mathf.Round(Random.Range(posMin, posMax)), 0, Mathf.Round(Random.Range(posMin, posMax)));
                }
                GameObject created = Instantiate(cubes[Random.Range(0,cubes.Length)], position, gameObject.transform.rotation);
                spawned.Add(created);
                j--;
            }

            yield return new WaitForSeconds(1);

            j = Random.Range(toSpawnMin, toSpawnMax);
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
