using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour
{
    public GameObject[] cubes;

    //Limite positivo e negativo per lo spawn di oggetti, rispetto a 0,0 (board di 10x10, posmin = -5, posmax = 5)
    public float posMin, posMax, spawnHeight;

    //Massimo e minimo di oggetti da spawnare
    public int toSpawnMin, toSpawnMax;

    //public int maxElements, seconds;

    List<GameObject> spawned;

    void Start()
    {
        spawned = new List<GameObject>();

        InitializeEnvironment();
        //StartCoroutine(DestroyElements(seconds));
    }

    //Inizializzazione statica
    void InitializeEnvironment()
    {
        int j = Random.Range(toSpawnMin, toSpawnMax);
        while (j > 0)
        {
            Vector3 position = randomPosition();
            while (spawned.Exists(cube => cube.transform.position == position))
            {
                position = randomPosition();
            }
            GameObject created = Instantiate(cubes[Random.Range(0, cubes.Length)], position, Random.rotation);
            spawned.Add(created);
            j--;
        }
    }

    Vector3 randomPosition ()
    {
        return new Vector3(Mathf.Round(Random.Range(posMin, posMax)), spawnHeight, Mathf.Round(Random.Range(posMin, posMax)));
    }

    ////Versione con aggiornamento periodico
    //IEnumerator DestroyElements(int seconds)
    //{
    //    int i = 0;
    //    while (i < 1000000) {

    //        InitializeEnvironment();

    //        yield return new WaitForSeconds(seconds);

    //        int j = Random.Range(toSpawnMin, toSpawnMax);
    //        while ((j > 0 || spawned.Count < maxElements) && spawned.Count > 0)
    //        {
    //            GameObject toDestroy = spawned[0];
    //            spawned.Remove(toDestroy);
    //            Destroy(toDestroy);
    //        }
    //    i++;
    //    }
    //}
}
