using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Entities.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField]
        protected Vector2 direction;
        protected abstract void Spawn();
    }
}

