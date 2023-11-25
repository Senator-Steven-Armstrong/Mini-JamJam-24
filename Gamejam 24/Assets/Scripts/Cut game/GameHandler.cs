using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> GameObjectsToEnableFirst;
    public List<GameObject> GameObjectsToEnableLater;
    public List<GameObject> GameObjectsToDisable;
    public float waitTime;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject Object in GameObjectsToDisable)
        {
            Object.SetActive(true);
        }
        foreach (GameObject Object in GameObjectsToEnableFirst)
        {
            Object.SetActive(false);
        }
        foreach (GameObject Object in GameObjectsToEnableLater)
        {
            Object.SetActive(false);
        }
        StartCoroutine(StartGame());
    }


    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject Object in GameObjectsToDisable)
        {
            Object.SetActive(false);
        }
        foreach (GameObject Object in GameObjectsToEnableFirst)
        {
            Object.SetActive(true);
        }
        music.Play();
        yield return new WaitForSeconds(2);
        foreach (GameObject Object in GameObjectsToEnableLater)
        {
            Object.SetActive(true);
        }
        
    }
}
