using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine;

namespace Jam.Entities
{
    public class HealthBase_Test : HealthBase
    {

        private void Awake()
        {
            OnTakeDamage += (dmg) => GetComponent<StateMachineController>().NotifyState(new Notification_Attacked());
        }
        protected override void Die()
        {
            
        }
    }

}

