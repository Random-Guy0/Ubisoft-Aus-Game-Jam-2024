using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Walker
{
    /// <summary>
    /// Only state for Walker type enemies
    /// </summary>
    /// <remarks>
    /// This enemy will simply walk in one direction and do nothing.
    /// This type is more of a background entity to populate the map.
    /// </remarks>
    public class State_Walker_Move : State<StateMachineController_Walker, Entity>
    {

        public override void OnEnter()
        {
            entity.RigidBody.velocity = controller.Direction * controller.Speed;
        }

        public override void OnExit()
        {

        }

        public override void OnNotify(Notification notification)
        {
            if(notification is Notification_Attacked)
            {
                entity.RigidBody.velocity = controller.Direction * controller.Speed * 1.8f;
            }

        }

        public override void OnUpdate()
        {
            
        }
    }

}
