using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro package
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    // vs TextMeshPro which is for text in the Game itself (Not GUI)
    [SerializeField] TextMeshProUGUI questionTextMeshGUI;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestionSO;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Answers")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        // Modify image fill based on calculated fraction
        timerImage.fillAmount = timer.fillFraction;

        if (timer.canLoadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.canLoadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            UpdateButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        UpdateButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswer(int index)
    {
        if (index == currentQuestionSO.GetCorrectAnswerIndex())
        {
            questionTextMeshGUI.text = "Correct!";
            highlightCorrectAnswer();
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            int correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestionSO.GetAnswer(correctAnswerIndex);

            questionTextMeshGUI.text = "Incorrect, the correct answer is:\n" + correctAnswer;

            highlightCorrectAnswer();
        }
    }

    void highlightCorrectAnswer()
    {
        int correctIndex = currentQuestionSO.GetCorrectAnswerIndex();

        // Grab Image component for current button obj
        Image buttonImage = answerButtons[correctIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            UpdateButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestionSO = questions[index];

        if (questions.Contains(currentQuestionSO))
        {
            questions.Remove(currentQuestionSO);
        }
    }

    void DisplayQuestion()
    {
        questionTextMeshGUI.text = currentQuestionSO.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextTextMeshGUI = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextTextMeshGUI.text = currentQuestionSO.GetAnswer(i);
        }
    }

    void UpdateButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button buttonGUI = answerButtons[i].GetComponent<Button>();
            buttonGUI.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
