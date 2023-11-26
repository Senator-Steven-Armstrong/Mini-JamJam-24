using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public List<GameObject> GameObjectsToEnableFirst;
    public List<GameObject> GameObjectsToEnableLater;
    public List<GameObject> GameObjectsToDisable;
    public float waitTime;
    public AudioSource music;
    public Text text;

    public int gameTime;
    public int sceneIndex;
    public AudioSource bigPling;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ScoreCalculator.totalScore);
        if(text != null)
        text.text = gameTime.ToString();
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
        StartCoroutine(StartGameTimer());
        music.Play();
        yield return new WaitForSeconds(2);
        foreach (GameObject Object in GameObjectsToEnableLater)
        {
            Object.SetActive(true);
        }
        
    }

    public IEnumerator StartGameTimer()
    {
        if (text != null)
            text.text = gameTime.ToString();
        if (gameTime <= 0)
        {
            Time.timeScale = 0;
            music.Stop();
            bigPling.Play();
            yield return new WaitForSecondsRealtime(3);
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            gameTime--;
            yield return new WaitForSeconds(1);
            StartCoroutine(StartGameTimer());
        }
        
       
    }
}
