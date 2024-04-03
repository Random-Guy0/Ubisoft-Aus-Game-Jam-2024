using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

namespace Jam.Entities
{
    /// <summary>
    /// Entity with NavMeshAgent.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavEntity : Entity 
    {
        protected NavMeshAgent agent;


        public NavMeshAgent NavMeshAgent { get { return agent; } set { agent = value; } }
        protected override void Awake()
        {
            base.Awake();

            agent = GetComponent<NavMeshAgent>();
            // Disable 3D-specific configurations from navmesh
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }



        /// <summary>
        /// Get a random sampled position within the radius.
        /// Assumes sample position exists within the radius.
        /// Otherwise it may return an invalid value.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="sampleRadius">Maximum radius allowed for sampling</param>
        /// <returns></returns>
        public virtual Vector2 GetSampledPosition(float radius, float sampleRadius = 1.0f)
        {
            NavMeshHit hit;
            Vector2 pos;

            int maxIter = 100, i = 0;
            do
            {
                pos = (Vector2)this.transform.position + Random.insideUnitCircle * radius;
                i++;
            }
            while (!NavMesh.SamplePosition(pos, out hit, sampleRadius, NavMesh.AllAreas) && i < maxIter);


            return hit.position;
        }
    }

}
