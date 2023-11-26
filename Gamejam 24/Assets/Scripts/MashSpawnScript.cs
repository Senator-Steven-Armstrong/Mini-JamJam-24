using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashSpawnScript : MonoBehaviour
{
    public List<GameObject> mashSpawns;
    void Start()
    {
        
    }

    private GameObject RandomSpawnPosition()
    {
        return mashSpawns[UnityEngine.Random.Range(0, mashSpawns.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
