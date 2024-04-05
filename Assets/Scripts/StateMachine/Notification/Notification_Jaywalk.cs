using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.StateMachine
{
    public class Notification_Jaywalk : Notification
    {
        BoxCollider2D endPoint;

        public Notification_Jaywalk(BoxCollider2D endPoint)
        { 
            this.endPoint = endPoint;
        }


        /// <summary>
        /// Get a randomised end point of the jaywalk
        /// </summary>
        /// <returns></returns>
        public Vector2 GetEndPoint()
        {
            return new Vector2(
                Random.Range(endPoint.bounds.min.x, endPoint.bounds.max.x),
                Random.Range(endPoint.bounds.min.y, endPoint.bounds.max.y));

        }
    }

}
