using System;
using System.Collections;
using System.Collections.Generic;
using Jam.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Slider trashSlider;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string scoreTextPrefix = "Score: ";

    private void Start()
    {
        trashSlider.maxValue = PlayManager.MAX_TRASH_COUNT;
        SetTrashSliderValue(PlayManager.Instance.TrashCount);
        SetScoreText(PlayManager.Instance.Score);

        PlayManager.Instance.OnTrashCountChange += SetTrashSliderValue;
        PlayManager.Instance.OnScoreChange += SetScoreText;
    }

    private void SetScoreText(int score)
    {
        scoreText.text = scoreTextPrefix + score;
    }

    private void SetTrashSliderValue(int value)
    {
        trashSlider.value = value;
    }
}
