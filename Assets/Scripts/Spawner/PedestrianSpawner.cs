using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Jam.Managers;
using Jam.StateMachine;

namespace Jam.Entities.Spawner
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PedestrianSpawner : Spawner
    {
        // Not elegant but works
        const int WALKER = 0;
        const int WALKING_TOSSER = 1;
        const int TOSSER = 2;

        private float walkingTosserProbability = 0.12f;
        private float tosserProbability = 0.07f;

        [SerializeField]
        GameObject walkerPrefab;

        [SerializeField]
        GameObject walkingTosserPrefab;

        [SerializeField]
        GameObject tosserPrefab;



        BoxCollider2D spawnBox;

        private int GetSpawnIndex()
        {
            float prob = Random.Range(0, 1.0f);

            if(prob < tosserProbability)
            {
                return TOSSER;
            }
            else if(tosserProbability <= prob && prob < tosserProbability + walkingTosserProbability)
            {
                return WALKING_TOSSER;
            }
            else
            {
                return WALKER;
            }
        }


        private Vector2 GetRandomPoint()
        {
            return new Vector2(
                Random.Range(spawnBox.bounds.min.x, spawnBox.bounds.max.x),
                Random.Range(spawnBox.bounds.min.y, spawnBox.bounds.max.y));
        }
       


        protected override void Spawn()
        {
            switch(GetSpawnIndex())
            {
                case WALKER:
                    if(PlayManager.Instance.AddWalker())
                    {
                        var obj = Instantiate(walkerPrefab);
                        obj.GetComponent<NavMeshAgent>().Warp(GetRandomPoint());
                        obj.GetComponent<StateMachineController>().Direction = direction;
                    }

                    break;
                case WALKING_TOSSER:
                    if (PlayManager.Instance.AddWalkingTosser())
                    {
                        var obj = Instantiate(walkingTosserPrefab);
                        obj.GetComponent<NavMeshAgent>().Warp(GetRandomPoint());
                        obj.GetComponent<StateMachineController>().Direction = direction;
                    }
                    break;
                case TOSSER:
                    if (PlayManager.Instance.AddTosser())
                    {
                        var obj = Instantiate(tosserPrefab);
                        obj.GetComponent<NavMeshAgent>().Warp(GetRandomPoint());
                        obj.GetComponent<StateMachineController>().Direction = direction;
                    }
                    break;
            }
        }

        private void Awake()
        {
            spawnBox = GetComponent<BoxCollider2D>();
            spawnBox.isTrigger = true;

            StartCoroutine(BeginSpawning());
        }


        IEnumerator BeginSpawning()
        {
            while(true)
            {
                float deviation = Random.Range(0.0f, 2.0f);
                yield return new WaitForSeconds(1.5f + deviation);

                Spawn();
            }

        }


    }
   
}

