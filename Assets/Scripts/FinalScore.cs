using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public int Score { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
