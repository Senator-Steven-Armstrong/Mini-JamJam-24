using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float timeElapsed;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        duration = 3;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float t = timeElapsed / duration;
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
        timeElapsed += Time.deltaTime;
        if (transform.localScale == Vector3.one)
        {
            Destroy(gameObject);
        }
    }
}
