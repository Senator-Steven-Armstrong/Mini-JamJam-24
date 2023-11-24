using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLineClick : MonoBehaviour
{
    public CutHandlerScript CHScript;

    // Start is called before the first frame update
    void Start()
    {
        CHScript = GameObject.Find("CutHandler").GetComponent<CutHandlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        CHScript.numOfCutLinesCut++;
        Debug.Log("cut : " + CHScript.numOfCutLinesCut);
        Destroy(gameObject);
    }
}
