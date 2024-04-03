using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;
using Jam.StateMachine.Generic;

namespace Jam.StateMachine.Tosser
{

    public class State_Tosser_Leave : State<StateMachineController_Tosser, Entity>
    {
        // Just walk in one direction for now.

        public override void OnEnter()
        {
            entity.RigidBody.velocity = Vector2.right * 3.0f;

            Debug.Log("Tosser: Leave", entity.gameObject);
        }

        public override void OnExit()
        {

        }

        public override void OnNotify(Notification notification)
        {
    
        }

        public override void OnUpdate()
        {
            
        }
    }
}
