using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerScript : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 GoalPosition;
    private float timeElapsed = 0;
    private float duration = 0.8f;
    public bool hasTouched;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        if(transform.GetChild(0).transform.rotation == Quaternion.Euler(0, 0, 90) || transform.GetChild(0).transform.rotation == Quaternion.Euler(0, 0, 270))
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
        if(!hasTouched)
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(StartPosition, GoalPosition, t);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(GoalPosition, StartPosition, t);
            timeElapsed += Time.deltaTime;
        }
        

        if(gameObject.transform.position == GoalPosition && !hasTouched)
        {
            StartCoroutine(DoSometingStupid());
        }
    }

    private IEnumerator DoSometingStupid()
    {
        yield return new WaitForSeconds(0.4f);
        if (!hasTouched)
        {
            timeElapsed = 0;
        }
        
        hasTouched = true;
    }

    private void OnMouseDown()
    {
        Debug.Log("ouchies!!");
        GoalPosition = transform.position;
        timeElapsed = 0;
        hasTouched = true;
    }
}
