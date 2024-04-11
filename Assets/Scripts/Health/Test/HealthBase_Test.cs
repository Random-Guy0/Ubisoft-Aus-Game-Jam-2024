using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine;

namespace Jam.Entities
{
    public class HealthBase_Test : HealthBase
    {

        protected override void Awake()
        {
            base.Awake();
            OnTakeDamage += (dmg) =>
            {
                GetComponent<StateMachineController>().NotifyState(new Notification_Attacked());
                Debug.Log(dmg);
            };

        }

        protected override void Die()
        {
            GetComponent<StateMachineController>().NotifyState(new Notification_HealthZero());
            Debug.Log("Triggered");
            
        }
    }

}

