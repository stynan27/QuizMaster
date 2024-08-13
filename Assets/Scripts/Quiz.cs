using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections; // TextMeshPro package

public class Quiz : MonoBehaviour
{
    [SerializeField]
    QuestionSO questionSO;

    // vs TextMeshPro which is for text in the Game itself (Not GUI)
    [SerializeField]
    TextMeshProUGUI questionTextMeshGUI;

    [SerializeField]
    GameObject[] answerButtons;

    void Start()
    {
        questionTextMeshGUI.text = questionSO.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextTextMeshGUI = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextTextMeshGUI.text = questionSO.GetAnswer(i);
        }
    }
}
