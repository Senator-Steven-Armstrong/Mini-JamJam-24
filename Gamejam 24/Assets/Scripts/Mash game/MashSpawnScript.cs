using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashSpawnScript : MonoBehaviour
{
    public List<GameObject> spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SelectRandomSpawn()
    {
        return spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count - 1)];
    }

    private IEnumerator Spawner()
    {

        yield return null;

        StartCoroutine(Spawner());
    }
}
