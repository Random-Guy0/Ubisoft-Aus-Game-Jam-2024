using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Walking_Tosser
{

    public class State_Walking_Tosser_Attacked : State<StateMachineController_Walking_Tosser, Entity>
    {
        float velocity = 8.0f;

        float panicDuration;

        float enterTime;

        float panic = 0.3f;
        float panicTime;

        public override void OnEnter()
        {
            Debug.Log("Imma panic");

            Vector2 dir = Random.insideUnitCircle.normalized;

            entity.RigidBody.velocity = dir * velocity;


            panicDuration = Random.Range(2.0f, 4.0f);
            enterTime = Time.time;

            panicTime = panic;
            
        }

        public override void OnExit()
        {
            entity.RigidBody.velocity = Vector2.zero;
        }

        public override void OnNotify(Notification notification)
        {

        }

        public override void OnUpdate()
        {

            if(Time.time - enterTime > panicTime)
            {
                panicTime += panic;
                Vector2 dir = Random.insideUnitCircle.normalized;
                entity.RigidBody.velocity = dir * velocity;
            }


            if(Time.time - enterTime > panicDuration)
            {
                controller.ChangeState(new State_Walking_Tosser_Walk());
            }
        }
    }

}

