using System;
using System.Collections;
using System.Collections.Generic;
using Jam.Managers;
using UnityEngine;

public class DestoyOnGameOver : MonoBehaviour
{
    private void Start()
    {
        PlayManager.Instance.OnGameOver += DestroySelf;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
