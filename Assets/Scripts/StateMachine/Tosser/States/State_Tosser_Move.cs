using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Tosser
{
    /// <summary>
    /// Tosser moves at a random direction for n seconds.
    /// </summary>
    public class State_Tosser_Move : State<StateMachineController_Tosser, NavEntity>
    {

        private float walkDuration = 1.5f;

        public override void OnEnter()
        {
            entity.NavMeshAgent.isStopped = false;

            if(!entity.NavMeshAgent.SetDestination(entity.GetSampledPosition(5.0f, 1.5f, 0.3f)))
            {
                Debug.LogWarning("NavMeshAgent failed to set a destination. This should not happen.", entity.gameObject);
            }


            entity.NavMeshAgent.speed = controller.Speed;
            Debug.Log("Tosser: Move", entity.gameObject);
        }

        public override void OnExit()
        {
            entity.NavMeshAgent.isStopped = true;
        }

        public override void OnNotify(Notification notification)
        {

            if(notification is Notification_Jaywalk && controller.Probability(0.3f))
            {
                controller.ChangeState(new State_Tosser_Jaywalk(notification as Notification_Jaywalk));
            }
            else if (notification is Notification_Attacked)
            {
                controller.ChangeState(new State_Tosser_Attacked());
            }



        }

        public override void OnUpdate()
        {
            if(GetTime() > walkDuration)
            {
                if (controller.ShouldForceToss())
                {
                    // Force transition to toss
                    controller.ChangeState(new State_Tosser_Toss());

                }
                else
                {
                    // Otherwise randomise.
                    if (controller.Probability(0.07f))
                    {
                        controller.ChangeState(new State_Tosser_Toss());
                    }
                    else
                    {
                        controller.ChangeState(new State_Tosser_Idle());
                    }

                }


            }

        }
    }

}
