using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine;

public class Notifier_Jaywalk : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D endPoint;

    private void Awake()
    {
        endPoint.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<StateMachineController>()?.NotifyState(new Notification_Jaywalk(endPoint));
    }


}
