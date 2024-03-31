using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.StateMachine
{
    public class StateMachine : ScriptableObject
    {
        /// <summary>
        /// entry state of the instanced state machine
        /// </summary>
        public virtual State EntryState { get { return null; } }


    }
}

