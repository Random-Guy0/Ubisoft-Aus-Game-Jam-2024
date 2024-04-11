using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using Jam.Entities;

using Jam.Entities.Enemy;
using Jam.Entities.Player;

using Jam.Managers;

namespace Jam.StateMachine.Vehicle
{
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StateMachineController_Vehicle : StateMachineController
    {
        NavMeshObstacle navObstacle;
        BoxCollider2D frontTriggerBox;


        protected override State entryState { get { return new State_Vehicle_Move(); } }

        public float MaxVelocity = 6.0f;

        float crashThreshold = 0.5f;    // Min speed required to fire crash event

        protected override void Awake()
        {
            base.Awake();

            navObstacle = GetComponent<NavMeshObstacle>();
            navObstacle.carveOnlyStationary = false;
            navObstacle.carving = true;
            
            frontTriggerBox = GetComponentInChildren<BoxCollider2D>();

            entity.RigidBody.mass = 1e10F;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(entity.RigidBody.velocity.magnitude > crashThreshold)
            {
                Player player;
                if(player = collision.gameObject.GetComponent<Player>())
                {
                    Debug.LogWarning("TODO: Player reaction to car crash", this);
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Enemy>() || collision.GetComponent<VehicleEntity>())
            {
                NotifyState(new Notification_Vehicle_Entity_InFront());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy>() || collision.GetComponent<VehicleEntity>())
            {
                NotifyState(new Notification_Vehicle_Entity_Away());
            }
        }

        public override void OnRemoved()
        {
            PlayManager.Instance.VehicleCount--;
        }
    }

}
