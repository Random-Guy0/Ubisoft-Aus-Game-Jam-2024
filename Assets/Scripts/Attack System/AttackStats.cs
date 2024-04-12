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

        [field: SerializeField] public float OriginOffset { get; private set; } = 0f;
        
        [field: SerializeField] public GameObject HitEffect { get; private set; }

        private Vector2 _direction;
        public Vector2 Direction
        {
            protected get => _direction;
            set
            {
                _direction = value;
            }
        }
        public Vector2 Origin { protected get; set; }

        protected List<Transform> targetsHit;
        
        public IEnumerator Attack(Vector2 direction, Vector2 origin)
        {
            Direction = direction;
            Origin = origin;
            
            yield return new WaitForSeconds(AttackStartDelay);

            GameObject hitEffectInstance = Instantiate(HitEffect);

            targetsHit = new List<Transform>();

            float attackTime = 0.0f;
            while (attackTime <= AttackDuration)
            {
                attackTime += Time.deltaTime;
                Vector2 position = GetAttackPosition(Direction, Origin);
                DealDamage(Direction, position);

                hitEffectInstance.transform.position = position;
                float angle = Vector2.SignedAngle(Vector2.right, Direction);
                hitEffectInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                
                yield return null;
            }

            yield return new WaitForSeconds(AttackEndDelay);
            Destroy(hitEffectInstance);
        }
        
        protected abstract void DealDamage(Vector2 direction, Vector2 position);

        private Vector2 GetAttackPosition(Vector2 direction, Vector2 origin)
        {
            Vector2 position = origin + direction * OriginOffset;
            return position;
        }
    }
}
