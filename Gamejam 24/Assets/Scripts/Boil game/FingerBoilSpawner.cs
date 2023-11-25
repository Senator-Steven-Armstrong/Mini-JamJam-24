using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBoilSpawner : MonoBehaviour
{
    public GameObject finger;
    public GameObject fruit;
    public float waitTime;
    private float _startingWaitTime;
    public float waitTimeFruit;
    private float _startingWaitTimeFruit;

    // Start is called before the first frame update
    void Start()
    {
        _startingWaitTime = waitTime;
        StartCoroutine(FingerSpawner());
        StartCoroutine(FruitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FingerSpawner()
    {
        if (_startingWaitTime == waitTime)
        {
            // Gör så att det inte spawnar fingarar i första 5 sek
            yield return new WaitForSeconds(4);
        }
        Instantiate(finger);
        if (waitTime >= 0.3f)
        {
            waitTime *= 0.9f;
            waitTime += Random.Range(-0.1f, 0.1f);
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FingerSpawner());
    }

    private IEnumerator FruitSpawner()
    {
        Instantiate(fruit);
        if (waitTimeFruit >= 0.3f)
        {
            waitTimeFruit *= 0.9f;
            waitTimeFruit += Random.Range(-0.2f, 0.2f);
        }
        yield return new WaitForSeconds(waitTimeFruit);
        StartCoroutine(FruitSpawner());
    }
}

