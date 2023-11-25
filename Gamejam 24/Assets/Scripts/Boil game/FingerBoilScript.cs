using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FingerBoilScript : MonoBehaviour
{

    private Vector2 GoalPosition = Vector2.zero;
    private Vector2 StartPosition;
    private float timeElapsed = 0;
    public float duration = 3;

    public bool isHeld;
    public bool isFruit;

    private BoilScoreHandler boilScoreHandler;


    // Start is called before the first frame update
    void Start()
    {
        boilScoreHandler = GameObject.Find("ScoreHandler").GetComponent<BoilScoreHandler>();
        duration = Random.Range(2, 5);
        transform.position = CalcPointOnCircle();
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            
            gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartPosition = transform.position;
            timeElapsed = 0;
            if (isFruit)
            {
                if (transform.position.x > 8.6f || transform.position.y > 4.6f || transform.position.x < -8.6f || transform.position.y < -4.6f)
                {
                    // OM FRUKTEN LÄMNAR SKÄRMEN
                    Debug.Log("bruh");
                    Destroy(gameObject);
                }
            }
            else
            {
                if (transform.position.x > 8.6f || transform.position.y > 4.6f || transform.position.x < -8.6f || transform.position.y < -4.6f)
                {
                    // OM HANDEN HAMNAR UTANFÖR SKÄRMEN
                    Debug.Log("Aw man..");
                    Destroy(gameObject);
                }
            }
            
        }
        else
        {
            float t = timeElapsed / duration;
            transform.position = Vector2.Lerp(StartPosition, GoalPosition, t);
            timeElapsed += Time.deltaTime;
        }

        if (!isFruit && transform.position.x < 1f && transform.position.y < 1f && transform.position.x > -1f && transform.position.y > -1f)
        {
            // OM HANDEN HAMNAR I KASTRULLEN
            Debug.Log("ouchies!!!");
            Destroy(gameObject);
            boilScoreHandler.points -= 100;
        }
        if (isFruit && transform.position.x < 1f && transform.position.y < 1f && transform.position.x > -1f && transform.position.y > -1f)
        {
            // OM FRUKTEN HAMNAR I KASTRULLEN
            Debug.Log("WE JAMMING");
            Destroy(gameObject);
            boilScoreHandler.points += 150;
        }

        //roterar mot center
        if (!isFruit)
        {
            Vector3 targ = Vector3.zero;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
    }

    private Vector3 CalcPointOnCircle()
    {
        Vector2 vector2;
        vector2 = Random.insideUnitCircle.normalized * 12;
        return vector2;
    }

    public void OnMouseDown()
    {
        isHeld = true;
        Debug.Log(gameObject.name + " is being held");
    }

    public void OnMouseUp()
    {
        isHeld = false;
    }
}