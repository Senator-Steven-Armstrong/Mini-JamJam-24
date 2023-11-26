using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashSpawnScript : MonoBehaviour
{
    public List<GameObject> spawnPositions;
    public float waitTime;
    private float _startingWaitTime;

    public List<GameObject> OccupiedPositions;
    public List<Vector3> availableSpawnPositions;

    public GameObject shadowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            availableSpawnPositions.Add(spawnPositions[i].transform.position);
        }
        _startingWaitTime = waitTime;
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Vector3> SelectRandomSpawnPos()
    {
        List<Vector3> result = new List<Vector3>();
        int numOfPositions;
        if (spawnPositions.Count >= 5 )
        {
            numOfPositions = UnityEngine.Random.Range(2, 5);
        }
        else
        {
            numOfPositions = UnityEngine.Random.Range(0, spawnPositions.Count);
        }
        
        for (int i = 0; i < numOfPositions; i++)
        {
            int index = UnityEngine.Random.Range(0, availableSpawnPositions.Count);
            result.Add(availableSpawnPositions[index]);
            availableSpawnPositions.RemoveAt(i);
        }
        return result;
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(waitTime);
        List<Vector3> chosenPositions = SelectRandomSpawnPos();
        for (int i = 0; i < chosenPositions.Count; i++)
        {
            Instantiate(shadowPrefab, chosenPositions[i], Quaternion.identity);
        }


        StartCoroutine(Spawner());
    }
}
