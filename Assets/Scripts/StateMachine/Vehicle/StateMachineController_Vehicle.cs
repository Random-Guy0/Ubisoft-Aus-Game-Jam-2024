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
        public AudioClip vehicleSound;
        public AudioSource audioSource;

        NavMeshObstacle navObstacle;
        BoxCollider2D frontTriggerBox;


        public Player player;
        public float soundRadius = 8.0f;


        protected override State entryState { get { return new State_Vehicle_Move(); } }

        public float MaxVelocity = 6.0f;

        float crashThreshold = 0.5f;    // Min speed required to fire crash event
        float knockBackIntensity = 8.0f;

        protected override void Awake()
        {
            base.Awake();

            navObstacle = GetComponent<NavMeshObstacle>();
            navObstacle.carveOnlyStationary = false;
            navObstacle.carving = true;
            
            frontTriggerBox = GetComponentInChildren<BoxCollider2D>();

            entity.RigidBody.mass = 1e10F;


            audioSource = SoundManager.Instance.PlaySound(vehicleSound, this.gameObject, destroyWhenDone: false);
            player = FindObjectOfType<Player>();

        }

        protected override void Update()
        {
            base.Update();

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(entity.RigidBody.velocity.magnitude > crashThreshold)
            {
                PlayerController player;

                if(player = collision.gameObject.GetComponent<PlayerController>())
                {
                    player.PlayStruck();

                    player.PlayerHasMovementControl = false;

                    Vector2 dir = player.gameObject.transform.position - gameObject.transform.position;

                    if(player.gameObject.transform.position.y > gameObject.transform.position.y)
                    {
                        dir.y = 1.0f;
                    }
                    else
                    {
                        dir.y = -1.0f;
                    }


                    dir.Normalize();

                    Player playerEntity = collision.gameObject.GetComponent<Player>();

                    playerEntity.RigidBody.velocity = dir * knockBackIntensity;




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
