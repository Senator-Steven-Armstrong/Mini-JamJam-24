using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoilScoreHandler : MonoBehaviour
{
    public int totalBerriesProcessed;
    public int points;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + points;
    }
}
