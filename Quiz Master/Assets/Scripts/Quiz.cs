using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> Questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    int correctAnswerIndex;
    bool hasAnsweredEarly = false;

    [Header("Buttons")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreKeeper scoreKeeper;

    [Header("Slider")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        progressBar.maxValue = Questions.Count;
        progressBar.value = 0;
        //QuestionSetup();
        //GetNextQuestion();
        //timer.loadnextquestion = true;
        //timer.isAnsweringQuestion = false;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadnextquestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadnextquestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            SetButtonState(false);
            DisplayAnswer(-1);
            scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        }
    }
    void GetNextQuestion()
    {
        if(Questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            QuestionSetup();
            progressBar.value++;
            scoreKeeper.incrementQuestionsSeen();
        }
        else
        {
            //PrintGameOver();
        }
    }
    public void QuestionSetup()
    {
        GetRandomQuestion();
        SetButtonState(true);
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++) 
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, Questions.Count);
        currentQuestion = Questions[index];
        if(Questions.Contains(currentQuestion))
            Questions.Remove(currentQuestion);
    }

    void SetDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    public void OnAnswerSelected(int i)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(i);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    public void DisplayAnswer(int i)
    {
        if(currentQuestion != null)
        {
            if(i == currentQuestion.GetAnswerIndex())
            {
                questionText.text = "Correct!";
                Image buttonImage = answerButtons[i].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
                scoreKeeper.incrementCorrectAnswers();
            }
            else
            {
                questionText.text = ":( answer was \"" + currentQuestion.GetAnswer(currentQuestion.GetAnswerIndex()) + ".\"";
                Image buttonImage = answerButtons[currentQuestion.GetAnswerIndex()].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            }
        }
    }
}
