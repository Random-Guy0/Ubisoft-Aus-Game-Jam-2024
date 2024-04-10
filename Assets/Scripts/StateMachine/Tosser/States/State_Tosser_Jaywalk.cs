using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Tosser
{
    public class State_Tosser_Jaywalk : State<StateMachineController_Tosser, NavEntity>
    {
        Notification_Jaywalk jaywalkNotification;
        Vector2 endPoint;

        public State_Tosser_Jaywalk(Notification_Jaywalk jaywalkNotification)
        {
            this.jaywalkNotification = jaywalkNotification;
            endPoint = jaywalkNotification.GetEndPoint();

        }


        public override void OnEnter()
        {
            entity.NavMeshAgent.isStopped = false;


            entity.NavMeshAgent.areaMask |= 1 << NavEntity.ROAD;
            entity.NavMeshAgent.SetDestination(endPoint);

            Debug.Log("Tosser: jaywalk");

        }

        public override void OnExit()
        {
            entity.NavMeshAgent.isStopped = true;
            entity.NavMeshAgent.areaMask &= ~(1 << NavEntity.ROAD);
        }

        public override void OnNotify(Notification notification)
        {

        }

        public override void OnUpdate()
        {
            if(Vector2.Distance(entity.transform.position, endPoint) < entity.NavMeshAgent.stoppingDistance)
            {
                controller.ChangeState(new State_Tosser_Move());

            }
        }
    }

}
