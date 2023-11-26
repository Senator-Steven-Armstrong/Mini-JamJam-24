using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MashSpawnScript : MonoBehaviour
{
    public List<GameObject> mashSpawns;
    public float spawnCooldown;
    public GameObject shadowPrefab;
    void Start()
    {
        StartCoroutine(RandomSpawnPosition());
    }

    private IEnumerator RandomSpawnPosition()
    {
        Instantiate(shadowPrefab,mashSpawns[UnityEngine.Random.Range(0, mashSpawns.Count - 1)].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(spawnCooldown);
        if (spawnCooldown >= 0.2f)
        {
            spawnCooldown -= 0.1f;
        }
        StartCoroutine(RandomSpawnPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
