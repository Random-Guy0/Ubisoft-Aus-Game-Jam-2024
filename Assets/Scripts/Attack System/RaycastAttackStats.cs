using System.Collections;
using System.Collections.Generic;
using Jam.Health;
using UnityEngine;

namespace Jam.AttackSystem
{
    [CreateAssetMenu(menuName = "Attacks/Raycast Attack", fileName = "new Raycast Attack")]
    public class RaycastAttackStats : AttackStats
    {
        [field: SerializeField] public float AttackDistance { get; private set; } = 1f;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        
        protected override void DealDamage(Vector2 direction, Vector2 position)
        {
            RaycastHit2D[] allHits = Physics2D.RaycastAll(position, direction, AttackDistance, LayerMask);
            
            #if UNITY_EDITOR
            Debug.DrawRay(position, direction * AttackDistance);
            #endif

            foreach (RaycastHit2D hit in allHits)
            {
                if (!targetsHit.Contains(hit.transform))
                {
                    if (hit.transform.TryGetComponent(out IDamageable hitObject))
                    {
                        hitObject.TakeDamage(Damage);
                        targetsHit.Add(hit.transform);   
                    }
                }
            }
        }
    }
}
