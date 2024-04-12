using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Jam.Entities; 
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Tosser
{
    public class State_Tosser_Attacked : State<StateMachineController_Tosser, Entity>
    {
        float velocityMultiplier = 1.2f;
        float velocity;

        float panicDuration;



        float panicInterval = 0.3f;
        float panicTime = 0.0f;

        public override void OnEnter()
        {
            Debug.Log("Imma panic");

            Vector2 dir = Random.insideUnitCircle.normalized;

            velocity = controller.Speed * velocityMultiplier;


            panicDuration = Random.Range(3.0f, 5.0f);


        }

        public override void OnExit()
        {
            entity.RigidBody.velocity = Vector2.zero;
        }

        public override void OnNotify(Notification notification)
        {
            if(notification is Notification_HealthZero)
            {
                controller.dizzyEffect.SetActive(true);
                controller.ChangeState(new State_Tosser_Leave());
            }

        }

        public override void OnUpdate()
        {

            if (GetTime() > panicTime)
            {
                panicTime += panicInterval;
                Vector2 dir = Random.insideUnitCircle.normalized;
                entity.RigidBody.velocity = dir * velocity;
            }


            if (GetTime() > panicDuration)
            {
                controller.ChangeState(new State_Tosser_Idle());
            }
        }
    }

}

