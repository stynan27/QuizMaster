using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using UnityEngine.UI; // TextMeshPro package

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO questionSO;

    // vs TextMeshPro which is for text in the Game itself (Not GUI)
    [SerializeField] TextMeshProUGUI questionTextMeshGUI;

    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    int correctAnswerIndex;

    void Start()
    {
        questionTextMeshGUI.text = questionSO.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextTextMeshGUI = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextTextMeshGUI.text = questionSO.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
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

    private void highlightCorrectAnswer()
    {
        int correctIndex = questionSO.GetCorrectAnswerIndex();

        // Grab Image component for current button obj
        Image buttonImage = answerButtons[correctIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
}
