using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")] // new attribute - adds an asset menu option to add an instance of this class
public class QuestionSO : ScriptableObject // new type inheritence - for storing game data
{
    [TextArea(2, 6)] // Adjust size of input text box
    [SerializeField]
    string question = "Enter a new question text here.";

    [SerializeField]
    string[] answers = new string[4];

    [SerializeField]
    int correctAnswerIndex = 0;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}