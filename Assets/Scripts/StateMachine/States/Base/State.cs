using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;

namespace Jam.StateMachine
{
    /// <summary>
    /// Base class for all states.
    /// </summary>
    /// <remarks>
    /// All methods are invoked by the state machine controller that owns the state.
    /// It may turn be stack-based or categorised in the future.
    /// </remarks>
    abstract public class State
    {
        /// <summary>
        /// Called automatically by StateMachine when transitioning to a different state.
        /// stateMachine will call OnExit of the previous state first and then this.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="entity"></param>
        public abstract void OnEnter(StateMachineController controller, Entity entity);

        /// <summary>
        /// Called automatically by StateMachine when transitioning to a different state.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="entity"></param>
        public abstract void OnExit(StateMachineController controller, Entity entity);
        /// <summary>
        /// Called continuously
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="entity"></param>
        public abstract void OnUpdate(StateMachineController controller, Entity entity);

        /// <summary>
        /// Called when event is fired by the state machine controller
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="entity"></param>
        /// <param name="notification"></param>
        public abstract void OnNotify(StateMachineController controller, Entity entity, Notification notification);
    }

}