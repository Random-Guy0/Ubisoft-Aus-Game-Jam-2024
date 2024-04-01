using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.AttackSystem
{
    public abstract class AttackStats : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; } = 5;

        [Tooltip("Time until the attack starts.")]
        [field: SerializeField]
        public float AttackStartDelay { get; private set; } = 0.3f;

        [Tooltip("The time the attack actually does damage for.")]
        [field: SerializeField]
        public float AttackDuration { get; private set; } = 0.1f;

        [Tooltip("Time after the attack but before the player is given control again")]
        [field: SerializeField]
        public float AttackEndDelay { get; private set; } = 0.2f;

        [field: SerializeField] public Vector2 OriginOffset { get; private set; } = Vector2.zero;

        public abstract IEnumerator Attack(Vector2 direction, Vector2 origin);
    }
}
