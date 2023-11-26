using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    public static int totalScore;
    public float moneys;
    public string grade;

    public AudioSource drumRoll;
    public AudioSource source;
    public AudioClip fard;
    public AudioClip boo;
    public AudioClip slowClap;
    public AudioClip bigClap;
    public AudioSource chaching;

    private bool _isCounting;
    private float _currentMoney;
    public Text text;

    public List<Sprite> gradeLetters;
    public Image gradeImage;

    public GameObject mysteryJam;
    public SpriteRenderer jamSprite;
    public List <Sprite> jams;
    public Vector3 startPos;
    public Vector3 endPos;
    public float timeElapsed = 0;
    public float duration = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        timeElapsed = 0;
        _currentMoney = 0;
        _isCounting = false;
        totalScore = -10000;
        Debug.Log(totalScore);
        StartCoroutine(DrumRoll());
    }

    // Update is called once per frame
    void Update()
    {
        if(mysteryJam.transform.position != endPos)
        {
            float t = timeElapsed / duration;
            mysteryJam.transform.position = Vector3.Lerp(mysteryJam.transform.position, endPos, t);
            timeElapsed += Time.deltaTime;
        }


        if (_isCounting)
        {
            if(_currentMoney <= moneys)
            {
                _currentMoney += 0.01f;
                text.text = (Mathf.Round(_currentMoney * 100) / 100).ToString() + "$";
            }
            else
            {
                if(moneys < 0)
                {
                    _currentMoney -= 0.01f;
                    text.text = (Mathf.Round(_currentMoney * 100) / 100).ToString() + "$";
                    if(moneys == _currentMoney)
                    {
                        Debug.Log("hit");
                        _currentMoney = moneys;
                        text.text = (Mathf.Round(moneys * 100) / 100).ToString() + "$";
                        _isCounting = false;
                        chaching.Play();
                    }
                }
                else
                {
                    _currentMoney = moneys;
                    text.text = (Mathf.Round(moneys * 100) / 100).ToString() + "$";
                    _isCounting = false;
                    chaching.Play();
                }
                
            }
            

        }
    }

    public float CalcMoney()
    {
        return totalScore / 1000 * 1.5f * Random.Range(0.85f, 2.26f);
    }

    public string CalcGrade()
    {
        gradeImage.color = new Color(1, 1, 1, 1);

        if(totalScore <= 0)
        {
            jamSprite.sprite = jams[0];
            gradeImage.sprite = gradeLetters[0];
            source.clip = fard;
            return "F";
        } 
        else if (totalScore >= 1000 && totalScore < 3000)
        {
            jamSprite.sprite = jams[1];
            gradeImage.sprite = gradeLetters[1];
            source.clip = boo;
            return "E";
        }
        else if (totalScore >= 3000 && totalScore < 4500)
        {
            jamSprite.sprite = jams[1];
            gradeImage.sprite = gradeLetters[2];
            source.clip = boo;
            return "D";
        }
        else if (totalScore >= 4500 && totalScore < 6000)
        {
            jamSprite.sprite = jams[2];
            gradeImage.sprite = gradeLetters[3];
            source.clip = slowClap;
            return "C";
        }
        else if (totalScore >= 6000 && totalScore < 9000)
        {
            jamSprite.sprite = jams[2];
            gradeImage.sprite = gradeLetters[4];
            source.clip = slowClap;
            return "B";
        }
        else if (totalScore >= 9000 && totalScore < 12000)
        {
            jamSprite.sprite = jams[3];
            gradeImage.sprite = gradeLetters[5];
            source.clip = bigClap;
            return "A";
        }
        else if (totalScore >= 12000)
        {
            jamSprite.sprite = jams[3];
            gradeImage.sprite = gradeLetters[6];
            source.clip = bigClap;
            return "S";
        }
        else
        {
            return "uuh not supposed to happen";
        }
        
    }

    public IEnumerator DrumRoll()
    {
        drumRoll.Play();
        yield return new WaitForSeconds(drumRoll.clip.length - 0.01f);
        grade = CalcGrade();
        moneys = CalcMoney();
        source.Play();
        yield return new WaitForSeconds(3f);
        _isCounting = true;
    }
}
