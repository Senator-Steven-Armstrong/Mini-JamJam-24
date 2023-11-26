using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLineClick : MonoBehaviour
{
    public CutHandlerScript CHScript;
    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        CHScript = GameObject.Find("CutHandler").GetComponent<CutHandlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameHandler.gameTime > 0)
        {
            CHScript.numOfCutLinesCut++;
            Debug.Log("cut : " + CHScript.numOfCutLinesCut);
            Destroy(gameObject);
        }
       
    }
}
