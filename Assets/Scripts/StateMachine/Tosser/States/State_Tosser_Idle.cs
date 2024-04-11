using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Tosser
{
    public class State_Tosser_Idle : State<StateMachineController_Tosser, Entity>
    {
        private float[] idleDurationRange = { 1.0f, 2.0f };

        private float idleDuration;

        public override void OnEnter()
        {
            idleDuration = Random.Range(idleDurationRange[0], idleDurationRange[1]);
            entity.RigidBody.velocity = Vector2.zero;

            Debug.Log("Tosser: Idle", entity.gameObject);
        }

        public override void OnExit()
        {

        }

        public override void OnNotify(Notification notification)
        {
            if(notification is Notification_Attacked)
            {
                controller.ChangeState(new State_Tosser_Attacked());
            }


        }

        public override void OnUpdate()
        {
            if(GetTime() > idleDuration)
            {

                controller.ChangeState(new State_Tosser_Move());
                controller.CurrentMoveTransitionCount++;

            }


        }
    }

}
