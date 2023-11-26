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

    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
        timeElapsed = 0;
        _currentMoney = 0;
        _isCounting = false;
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
            if(_currentMoney <= moneys && moneys >= 0)
            {
                _currentMoney += 0.01f;
                text.text = "Profit: " + (Mathf.Round(_currentMoney * 100) / 100).ToString() + "$";
            }
            else if (_currentMoney >= moneys && moneys < 0)
            {
               
                _currentMoney -= 0.01f;
                text.text = "Profit: " + (Mathf.Round(_currentMoney * 100) / 100).ToString() + "$";
               

            }
            else
            {
                _currentMoney = moneys;
                text.text = "Profit: " + (Mathf.Round(moneys * 100) / 100).ToString() + "$";
                _isCounting = false;
                chaching.Play();
                StartCoroutine(EnableReturnButton());
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

        if(totalScore < 1000)
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
            source.clip = fard;
            return "E";
        }
        else if (totalScore >= 3000 && totalScore < 4000)
        {
            jamSprite.sprite = jams[1];
            gradeImage.sprite = gradeLetters[2];
            source.clip = boo;
            return "D";
        }
        else if (totalScore >= 4000 && totalScore < 5000)
        {
            jamSprite.sprite = jams[2];
            gradeImage.sprite = gradeLetters[3];
            source.clip = slowClap;
            return "C";
        }
        else if (totalScore >= 5000 && totalScore < 6000)
        {
            jamSprite.sprite = jams[2];
            gradeImage.sprite = gradeLetters[4];
            source.clip = slowClap;
            moneys *= 2;
            return "B";
        }
        else if (totalScore >= 6000 && totalScore < 7500)
        {
            jamSprite.sprite = jams[3];
            gradeImage.sprite = gradeLetters[5];
            source.clip = bigClap;
            moneys *= 5;
            return "A";
        }
        else if (totalScore >= 7500)
        {
            jamSprite.sprite = jams[3];
            gradeImage.sprite = gradeLetters[6];
            source.clip = bigClap;
            moneys *= 8;
            return "S";
        }
        else
        {
            Debug.Log("ooops");
            return "uuh not supposed to happen";
        }
        
    }

    public IEnumerator DrumRoll()
    {
        drumRoll.Play();
        yield return new WaitForSeconds(drumRoll.clip.length - 0.01f);
        moneys = CalcMoney();
        grade = CalcGrade();
        if (moneys > PlayerPrefs.GetFloat("HighScore")){
            PlayerPrefs.SetFloat("HighScore", moneys);
        }
        
        source.Play();
        yield return new WaitForSeconds(3f);
        _isCounting = true;
    }

    public IEnumerator EnableReturnButton()
    {
        yield return new WaitForSeconds(2);
        button.SetActive(true);
    }
}
