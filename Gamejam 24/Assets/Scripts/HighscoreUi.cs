using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUi : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Largest Profit: " + Mathf.Round(PlayerPrefs.GetFloat("HighScore") * 100) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
