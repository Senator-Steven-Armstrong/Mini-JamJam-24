using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FingerBoilScript : MonoBehaviour
{

    private Vector2 GoalPosition = Vector2.zero;
    private Vector2 StartPosition;
    private float timeElapsed = 0;
    public float duration = 1;

    public bool isHeld;
    public bool isFruit;
    public bool isHurt;
    public Sprite hurtSprite;
    public SpriteRenderer hurtRenderer;

    private BoilScoreHandler boilScoreHandler;


    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
        hurtRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        boilScoreHandler = GameObject.Find("ScoreHandler").GetComponent<BoilScoreHandler>();
        if (isFruit)
        {
            duration = Random.Range(1.2f, 2.5f);
        }
        else
        {
            duration = Random.Range(0.8f, 1.5f);
        }
        
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
            // gör så att handen flyger tillbaka mot kanten när den är skadad
            if (isHurt)
            {
                float t = timeElapsed / duration;
                gameObject.transform.position = Vector2.Lerp(GoalPosition, StartPosition.normalized * 20, t);
                timeElapsed += Time.deltaTime;
                if (transform.position.x > 10f || transform.position.y > 6f || transform.position.x < -10f || transform.position.y < -6f)
                {
                    Debug.Log("DIE");
                    Destroy(gameObject);
                }
            }
            else
            {
                float t = timeElapsed / duration;
                transform.position = Vector2.Lerp(StartPosition, GoalPosition, t);
                timeElapsed += Time.deltaTime;
            }
            
        }
        if (!isFruit && transform.position.x < 1f && transform.position.y < 1f && transform.position.x > -1f && transform.position.y > -1f && !isHurt)
        {
            // OM HANDEN HAMNAR I KASTRULLEN
            Debug.Log("ouchies!!!");
            isHeld = false;
            
            gameObject.GetComponent<Collider2D>().enabled = false;
            hurtRenderer.sprite = hurtSprite;
            StartCoroutine(HurtAnimation());
            ScoreCalculator.totalScore -= 200;
            boilScoreHandler.points -= 200;
            timeElapsed = 0;
            duration = 1.8f;
            isHurt = true;
        }
        if (isFruit && transform.position.x < 1f && transform.position.y < 1f && transform.position.x > -1f && transform.position.y > -1f)
        {
            // OM FRUKTEN HAMNAR I KASTRULLEN
            Debug.Log("WE JAMMING");
            Destroy(gameObject);
            ScoreCalculator.totalScore += 100;
            boilScoreHandler.points += 100;
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

    private IEnumerator HurtAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        hurtRenderer.flipX = !hurtRenderer.flipX;
        StartCoroutine(HurtAnimation());
    }
}