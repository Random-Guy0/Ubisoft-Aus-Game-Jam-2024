using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;

namespace Jam.StateMachine
{
    /// <summary>
    /// Genericless base class. 
    /// </summary>
    abstract public class State
    {
        public abstract void OnInit(StateMachineController controller, Entity entity);

        /// <summary>
        /// Called automatically by StateMachine when transitioning to a different state.
        /// stateMachine will call OnExit of the previous state first and then this.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// Called automatically by StateMachine when transitioning to a different state.
        /// </summary>
        public abstract void OnExit();
        /// <summary>
        /// Called continuously
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Called when event is fired by the state machine controller
        /// </summary>
        /// <param name="notification"></param>
        public abstract void OnNotify(Notification notification);
    }

 
    namespace Generic
    {
        /// <summary>
        /// Base class for all states.
        /// </summary>
        /// <remarks>
        /// All methods are invoked by the state machine controller that owns the state.
        /// It may turn be stack-based or categorised in the future.
        /// </remarks>
        /// <typeparam name="T">Type of the controller</typeparam>
        /// <typeparam name="E">Type of the entity</typeparam>
        abstract public class State<T, E> : State where T : StateMachineController where E : Entity
        {
            protected T controller;
            protected E entity;

            /// <summary>
            /// Call this before any other methods to supply context.
            /// </summary>
            /// <param name="controller"></param>
            /// <param name="entity"></param>
            public override void OnInit(StateMachineController controller, Entity entity)
            {
                this.controller = (T)controller;
                this.entity = (E)entity;
            }

        }

    }

}