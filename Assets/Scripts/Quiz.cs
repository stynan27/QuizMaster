using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro package
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO questionSO;
    // vs TextMeshPro which is for text in the Game itself (Not GUI)
    [SerializeField] TextMeshProUGUI questionTextMeshGUI;

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


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestion();
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
    }

    void DisplayAnswer(int index)
    {
        if (index == questionSO.GetCorrectAnswerIndex())
        {
            questionTextMeshGUI.text = "Correct!";
            highlightCorrectAnswer();
        }
        else
        {
            int correctAnswerIndex = questionSO.GetCorrectAnswerIndex();
            string correctAnswer = questionSO.GetAnswer(correctAnswerIndex);

            questionTextMeshGUI.text = "Incorrect, the correct answer is:\n" + correctAnswer;

            highlightCorrectAnswer();
        }
    }

    void highlightCorrectAnswer()
    {
        int correctIndex = questionSO.GetCorrectAnswerIndex();

        // Grab Image component for current button obj
        Image buttonImage = answerButtons[correctIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }

    void GetNextQuestion()
    {
        UpdateButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionTextMeshGUI.text = questionSO.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextTextMeshGUI = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextTextMeshGUI.text = questionSO.GetAnswer(i);
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
