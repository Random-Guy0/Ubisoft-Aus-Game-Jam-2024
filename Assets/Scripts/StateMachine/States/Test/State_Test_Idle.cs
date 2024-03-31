using Jam.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.StateMachine.Test
{
    /// <summary>
    /// After [1, 4] seconds, transition to State_Test_Move
    /// </summary>
    /// 
    public class State_Test_Idle : State
    {
        float enteredTime;
        float threshold;
        public override void OnEnter(StateMachineController controller, Entity entity)
        {
            threshold = Random.Range(1f, 4f);
            enteredTime = Time.time;
        }

        public override void OnExit(StateMachineController controller, Entity entity)
        {
           
        }

        public override void OnNotify(StateMachineController controller, Entity entity, Notification notification)
        {
          // Consume reset notification and reset the position
          if(notification is Notification_Test_Reset)
            {
                entity.transform.position = Vector2.zero;
            }
        }

        public override void OnUpdate(StateMachineController controller, Entity entity)
        {
            if(Time.time - enteredTime >= threshold)
            {
               controller.ChangeState(new State_Test_Move());
            }
        }
    }

}

