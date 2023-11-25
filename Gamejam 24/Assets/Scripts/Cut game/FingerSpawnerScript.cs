using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FingerSpawnerScript : MonoBehaviour
{
    public List<GameObject> Spawns;
    public float waitTime;
    private float _startingWaitTime;
    public GameObject FingerPrefab;
    public GameObject FingerSidewaysPrefab;
    public Text iWonder;
    private string _text = "Man, I wonder what would happen if I just..";
    public int fingersSpawnedTotal;

    // Start is called before the first frame update
    void Start()
    {
        iWonder.text = "";
        _startingWaitTime = waitTime;
        StartCoroutine(Spawner());
        StartCoroutine(SpawnText());
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
            yield return new WaitForSeconds(7);
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
        fingersSpawnedTotal++;
        if(_startingWaitTime == waitTime)
        {
            yield return new WaitForSeconds(1.5f);
        }
        if(waitTime >= 0.7f)
        waitTime *= 0.9f;
        StartCoroutine(Spawner());
    }

    private IEnumerator SpawnText()
    {
        yield return new WaitForSeconds(7);
        iWonder.text = _text;
        yield return new WaitForSeconds(waitTime);
        iWonder.text = "";
    }
}
