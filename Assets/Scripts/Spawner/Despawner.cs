using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;
using Jam.StateMachine;

namespace Jam.Entities.Spawner
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Despawner : MonoBehaviour
    {
        BoxCollider2D col;

        private void Awake()
        {
            col = GetComponent<BoxCollider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StateMachineController controller;
            if(controller = collision.GetComponent<StateMachineController>())
            {
                controller.OnRemoved();
                Destroy(controller.gameObject);
            }
            
        }
    }

}
