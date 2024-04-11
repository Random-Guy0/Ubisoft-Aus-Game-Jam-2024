using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Managers;
using Jam.StateMachine;

namespace Jam.Entities.Spawner
{
    public class VehicleSpawner : Spawner
    {

        [SerializeField]
        GameObject vehiclePrefab;

        protected override void Spawn()
        {
            if(PlayManager.Instance.AddVehicle())
            {
                var obj = Instantiate(vehiclePrefab);
                obj.GetComponent<StateMachineController>().Direction = direction;
                obj.transform.position = this.transform.position;
            }

        }


        private void Awake()
        {
            StartCoroutine(BeginSpawning());
        }


        IEnumerator BeginSpawning()
        {
            while (true)
            {
                float deviation = Random.Range(0.0f, 2.0f);
                yield return new WaitForSeconds(2.8f + deviation);

                Spawn();
            }

        }

    }

}
