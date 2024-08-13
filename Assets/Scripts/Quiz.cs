using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro package

public class Quiz : MonoBehaviour
{
    [SerializeField]
    QuestionSO question;

    // vs TextMeshPro which is for text in the Game itself (Not GUI)
    [SerializeField]
    TextMeshProUGUI questionText;

    void Start()
    {
        questionText.text = question.GetQuestion();
    }
}
