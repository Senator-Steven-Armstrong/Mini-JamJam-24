using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedText : MonoBehaviour
{
    public Text text;
    public string stringasdjaksd = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnText());
    }

    public IEnumerator spawnText()
    {
        text.text = "";
        yield return new WaitForSeconds(1);
        text.text = stringasdjaksd;
    }
}
