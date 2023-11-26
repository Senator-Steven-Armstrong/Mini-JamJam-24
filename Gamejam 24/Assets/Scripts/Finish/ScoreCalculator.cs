using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static int totalScore;
    public float moneys;
    public string grade;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(totalScore);
        moneys = CalcMoney();
        grade = CalcGrade();
        Debug.Log(moneys + "$");
        Debug.Log(grade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalcMoney()
    {
        return totalScore / 1000 * 1.5f;
    }

    public string CalcGrade()
    {
        if(totalScore <= 0)
        {
            return "F";
        } 
        else if (totalScore >= 1000 && totalScore < 3000)
        {
            return "E";
        }
        else if (totalScore >= 3000 && totalScore < 45000)
        {
            return "D";
        }
        else if (totalScore >= 4500 && totalScore < 6000)
        {
            return "C";
        }
        else if (totalScore >= 6000 && totalScore < 9000)
        {
            return "B";
        }
        else if (totalScore >= 9000 && totalScore < 12000)
        {
            return "A";
        }
        else if (totalScore >= 12000)
        {
            return "S";
        }
        else
        {
            return "uuh not supposed to happen";
        }
        
    }
}
