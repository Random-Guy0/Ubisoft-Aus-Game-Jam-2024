using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.StateMachine
{
    /// <summary>
    /// Base class for notification for state machine.
    /// </summary>
    /// 
    /// <remarks>
    /// When a notification is fired using StateMachineController.NotifyState(),
    /// it will alert the current state of the controller of that notification.
    /// 
    /// State will react to the notification according to its defined behaviour.
    /// 
    /// This class may expand so that it can force a transition.
    /// </remarks>
    public abstract class Notification 
    {
    }
}

