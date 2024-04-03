using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Tosser
{
    public class State_Tosser_Toss : State<StateMachineController_Tosser, Entity>
    {
        private float[] tossDurationRange = { 1.0f, 3.0f };
        private float tossDuration;

        public override void OnEnter()
        {
            entity.RigidBody.velocity = Vector2.zero;
            tossDuration = Random.Range(tossDurationRange[0], tossDurationRange[1]);

            Debug.Log("Tosser: Toss", entity.gameObject);

        }

        public override void OnExit()
        {

        }

        public override void OnNotify(Notification notification)
        {

        }

        public override void OnUpdate()
        {
            if(GetTime() > tossDuration)
            {
                // Toss a trash and move away
                controller.TossTrash();
                controller.TossCount--;
                controller.CurrentMoveTransitionCount = 0;

                if(controller.TossCount <= 0)
                {
                    // Transition to Leaving, no trash remaining.
                    controller.ChangeState(new State_Tosser_Leave());
                }
                else
                {
                    // Walk away from the trash
                    controller.ChangeState(new State_Tosser_Move());
                }

            }
        }

    }

}
