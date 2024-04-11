using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

using Jam.Entities;

namespace Jam.StateMachine
{
    /// <summary>
    /// Base class for all state machine controllers
    /// </summary>
    /// <remarks>
    /// Derived controllers define internal variables and override the currentState property.
    /// </remarks>
    public abstract class StateMachineController : MonoBehaviour
    {
        /// <summary>
        /// Entry state of the state machine
        /// </summary>
        protected virtual State entryState { get { return null; } }
       
        
        protected Entity entity;
        protected State currentState = null;
        protected Vector2 direction = Vector2.right;

        public Vector2 Direction { get { return direction; } set { direction = value; } }


        /// <summary>
        /// Call this before OnDestory() to decrement entity count.
        /// Can't directly do this in OnDestory() otherwise Unity will complain.
        /// </summary>
        public abstract void OnRemoved();


        protected virtual void Awake()
        {
            
            entity = GetComponent<Entity>();

            currentState = entryState;

            // Make sure the state machine has an entry point
            if(currentState == null)
            {
                Debug.LogError("STATE_MACHINE_CONTROLLER: provided state machine has no entry point!", this.gameObject);
            }

            currentState.OnInit(this, entity);
        }




        // Start is called before the first frame update
        protected virtual void Start()
        {
            currentState.OnEnter();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            currentState.OnUpdate();
        }

        /// <summary>
        /// Transition to a new state, calling OnExit and OnEnter from previous and new state sequentially.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnInit(this, entity);
            currentState.OnEnter();
        }

        /// <summary>
        /// Notify the current state of external event.
        /// </summary>
        /// <param name="notification"></param>
        public void NotifyState(Notification notification)
        {
            currentState.OnNotify(notification);
        }

        /// <summary>
        /// Probability for probability driven statemachine
        /// </summary>
        /// <param name="likeliness"></param>
        /// <returns></returns>
        public bool Probability(float likeliness)
        {
            return Random.Range(0.0f, 1.0f) <= likeliness;
        }

    }

}