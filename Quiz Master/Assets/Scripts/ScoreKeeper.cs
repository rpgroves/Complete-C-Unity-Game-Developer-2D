using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int CalculateScore()
    {
        return Mathf.RoundToInt((float)correctAnswers / questionsSeen * 100);
    }
    
    public int getCorrectAnswers()
    {
        return correctAnswers;
    }

    public void incrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int getQuestionsSeen()
    {
        return questionsSeen;
    }

    public void incrementQuestionsSeen()
    {
        questionsSeen++;
    }
}
