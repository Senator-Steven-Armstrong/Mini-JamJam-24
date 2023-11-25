using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerScript : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 GoalPosition = new Vector3();
    private float timeElapsed;
    private float duration;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        if(transform.rotation == Quaternion.Euler(0, 0, 90) || transform.rotation == Quaternion.Euler(0, 0, 270))
        {
            GoalPosition = Vector3.zero;
        }
        else
        {
            GoalPosition = transform.position;
            GoalPosition.y = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float t = timeElapsed / duration;
        gameObject.transform.position = Vector3.Lerp(StartPosition, GoalPosition, t);
        timeElapsed += Time.deltaTime;
    }
}
