using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;

namespace Jam.StateMachine
{

    [RequireComponent(typeof(Entity))]
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField]
        private StateMachine stateMachine;

        private Entity entity;
        private State currentState = null;



        private void Awake()
        {
            entity = GetComponent<Entity>();
            currentState = stateMachine.EntryState;

            // Make sure the state machine has an entry point
            if(currentState == null)
            {
                Debug.LogError("STATE_MACHINE_CONTROLLER: provided state machine has no entry point!", this.gameObject);
            }

        }

        // Start is called before the first frame update
        void Start()
        {
            currentState.OnEnter(this, entity);
        }

        // Update is called once per frame
        void Update()
        {
            currentState.OnUpdate(this, entity);
        }

        /// <summary>
        /// Transition to a new state, calling OnExit and OnEnter from previous and new state sequentially.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            currentState.OnExit(this, entity);
            currentState = state;
            currentState.OnEnter(this, entity);
        }

        /// <summary>
        /// Notify the current state of external event.
        /// </summary>
        /// <param name="notification"></param>
        public void NotifyState(Notification notification)
        {
            currentState.OnNotify(this, entity, notification);
        }

    }

}