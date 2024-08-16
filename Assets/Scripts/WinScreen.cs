using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreTextMeshGUI;
    ScoreKeeper scoreKeeper;

    // Lifecycle event occurs once before Start() on GameObject creation
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreTextMeshGUI.text = "Congratulations!\nYou scored " +
            scoreKeeper.CalculateScore() + "%";
    }
}
