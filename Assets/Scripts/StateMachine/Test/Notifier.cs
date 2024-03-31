using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Test;
using Jam.StateMachine;

/// <summary>
/// When Q is pressed, notify the state machine of Notification_Test_Reset.
/// </summary>
/// 
public class Notifier : MonoBehaviour
{
    // Start is called before the first frame update
    StateMachineController c;

    private void Awake()
    {
        c = FindObjectOfType<StateMachineController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            c.NotifyState(new Notification_Test_Reset());
        }
    }
}
