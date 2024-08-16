using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    WinScreen winScreen;

    // Use Awake for find methods (before game start)
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        winScreen = FindObjectOfType<WinScreen>();
    }

    // set initial Obj state in Start()
    void Start()
    {
        quiz.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isQuizComplete)
        {
            quiz.gameObject.SetActive(false);
            winScreen.gameObject.SetActive(true);
            winScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
