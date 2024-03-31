using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.StateMachine
{
    /// <summary>
    /// State machine for testing purpose
    /// </summary>
    [CreateAssetMenu(fileName= "StateMachine_Test", menuName= "StateMachine/Test")]
    public class TestingStateMachine : StateMachine
    {
        public override State EntryState { get { return new Test.State_Test_Idle(); } }
    }

}