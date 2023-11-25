using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSpawnerScript : MonoBehaviour
{
    public List<GameObject> Spawns;
    public float waitTime;
    private float _startingWaitTime;
    public GameObject FingerPrefab;
    public GameObject FingerSidewaysPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _startingWaitTime = waitTime;
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SelectRandomSpawn()
    {
        return Spawns[Random.Range(0, Spawns.Count-1)];
    }

    private IEnumerator Spawner()
    {
        if(_startingWaitTime == waitTime)
        {
            // Gör så att det inte spawnar fingarar i första 5 sek
            //yield return new WaitForSeconds(5);
        }
        yield return new WaitForSeconds(waitTime);
        GameObject RandomSpawn = SelectRandomSpawn();
        if(RandomSpawn.transform.rotation == Quaternion.Euler(0, 0, 90) || RandomSpawn.transform.rotation == Quaternion.Euler(0, 0, 270))
        {
            GameObject Finger = Instantiate(FingerSidewaysPrefab, RandomSpawn.transform.position, Quaternion.identity);
            Finger.transform.GetChild(0).gameObject.transform.rotation = RandomSpawn.transform.rotation;
        }
        else
        {
            GameObject Finger = Instantiate(FingerPrefab, RandomSpawn.transform.position, Quaternion.identity);
            Finger.transform.GetChild(0).gameObject.transform.rotation = RandomSpawn.transform.rotation;
        }
        if(waitTime >= 0.3f)
        waitTime -= 0.05f;
        StartCoroutine(Spawner());
    }
}
