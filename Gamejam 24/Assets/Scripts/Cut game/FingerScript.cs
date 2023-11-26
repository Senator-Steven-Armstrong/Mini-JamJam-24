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
    private BoilScoreHandler boilScoreHandler;
    private bool hasBeenCut;
    private FingerSpawnerScript spawner;
    public Sprite hurtSprite;
    public SpriteRenderer hurtRenderer;
    public AudioSource ouchies;
    public List<AudioClip> sounds;
    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        hasBeenCut = false;
        boilScoreHandler = GameObject.Find("ScoreHandler").GetComponent<BoilScoreHandler>();
        spawner = GameObject.Find("Finger Spawner").GetComponent<FingerSpawnerScript>();
        hurtRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
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
        if(spawner.fingersSpawnedTotal == 1)
        {
            duration = 3f;
        }

        duration = Random.Range(0.5f, 1f);
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

        if(gameObject.transform.position == StartPosition && hasTouched)
        {
            Destroy(gameObject);
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
        if (!hasBeenCut && gameHandler.gameTime > 0)
        {

            Debug.Log("ouchies!!");
            ouchies.clip = sounds[Random.Range(0, sounds.Count)];
            ouchies.Play();
            GoalPosition = transform.position;
            timeElapsed = 0;
            hasTouched = true;
            ScoreCalculator.totalScore -= 70;
            boilScoreHandler.points -= 70;
            hasBeenCut = true;
            hurtRenderer.sprite = hurtSprite;
            duration = 5;
            StartPosition *= 5;
            StartCoroutine(HurtAnimation());
        }
    }

    private IEnumerator HurtAnimation() 
    {
        yield return new WaitForSeconds(0.2f);
        hurtRenderer.flipX = !hurtRenderer.flipX;
        StartCoroutine(HurtAnimation());
    }
}
