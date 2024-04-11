using System.Collections;
using System.Collections.Generic;
using Jam.Interfaces;
using UnityEngine;

namespace Jam.AttackSystem
{
    [CreateAssetMenu(menuName = "Attacks/BoxCast Attack", fileName = "new BoxCast Attack")]
    public class BoxCastAttackStats : AttackStats
    {
        [field: SerializeField] public Vector2 AttackSize { get; private set; } = Vector2.one;
        [field: SerializeField] public float AttackDistance { get; private set; } = 1f;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        
        protected override void DealDamage(Vector2 direction, Vector2 position)
        {
            position += direction * AttackDistance;
            RaycastHit2D[] allHits = Physics2D.BoxCastAll(position, AttackSize, 0f, direction, AttackDistance, LayerMask);
            
            #if UNITY_EDITOR
            Debug.DrawLine(new Vector3(position.x + AttackSize.x * 0.5f, position.y + AttackSize.y * 0.5f), new Vector3(position.x - AttackSize.x * 0.5f, position.y + AttackSize.y * 0.5f), Color.green);
            Debug.DrawLine(new Vector3(position.x + AttackSize.x * 0.5f, position.y + AttackSize.y * 0.5f), new Vector3(position.x + AttackSize.x * 0.5f, position.y - AttackSize.y * 0.5f), Color.green);
            Debug.DrawLine(new Vector3(position.x + AttackSize.x * 0.5f, position.y - AttackSize.y * 0.5f), new Vector3(position.x - AttackSize.x * 0.5f, position.y - AttackSize.y * 0.5f), Color.green);
            Debug.DrawLine(new Vector3(position.x - AttackSize.x * 0.5f, position.y + AttackSize.y * 0.5f), new Vector3(position.x - AttackSize.x * 0.5f, position.y - AttackSize.y * 0.5f), Color.green); 
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
