using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.StateMachine.Generic;
using Jam.Entities;

namespace Jam.StateMachine.Tosser
{
    public class State_Tosser_Idle : State<StateMachineController_Tosser, Entity>
    {
        private float[] idleDurationRange = { 2.0f, 6.0f };

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

        }

        public override void OnUpdate()
        {
            if(GetTime() > idleDuration)
            {

                if (controller.ShouldForceToss())
                {
                    // Force transition to toss
                    controller.ChangeState(new State_Tosser_Toss());
                    
                }
                else
                {
                    // Otherwise randomise.
                    if (controller.ShouldTossByRandom(0f))
                    {

                    }
                    else
                    {
                        controller.ChangeState(new State_Tosser_Move());
                        controller.CurrentMoveTransitionCount++;
                    }



                }




            }


        }
    }

}
