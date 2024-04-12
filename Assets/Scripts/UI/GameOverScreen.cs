using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private string finalScoreTextPrefix = "Final Score: ";

    private void Start()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        finalScoreText.text = finalScoreTextPrefix + finalScore.Score;
        Destroy(finalScore);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.rKey.isPressed)
        {
            SceneManager.LoadScene(1);
        }
        else if (keyboard.escapeKey.isPressed)
        {
            SceneManager.LoadScene(0);
        }
    }
}
