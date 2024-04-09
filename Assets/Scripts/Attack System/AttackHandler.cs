using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.AttackSystem
{
    public abstract class AttackHandler : MonoBehaviour
    {
        public bool CanAttack { get; set; } = true;

        private Vector2 _attackOrigin;
        public Vector2 AttackOrigin
        {
            protected get => _attackOrigin;
            set
            {
                _attackOrigin = value;
                SelectedAttack.Origin = value;
            }
        }

        public Vector2 AttackDirection { get; set; }

        [SerializeField] protected AttackStats[] attacks;

        private AttackStats _selectedAttack;
        protected AttackStats SelectedAttack
        {
            get => _selectedAttack;
            set => _selectedAttack = Instantiate<AttackStats>(value);
        }

        protected virtual void Awake()
        {
            SelectedAttack = attacks[0];
        }

        private IEnumerator Attack()
        {
            BeforeAttack();
            
            CanAttack = false;
                
            Vector2 origin = AttackOrigin;
            Vector2 direction = AttackDirection;
                
            yield return SelectedAttack.Attack(direction, origin);
            CanAttack = true;
                
            AfterAttack();
        }

        protected virtual void BeforeAttack()
        {
            
        }

        protected virtual void AfterAttack()
        {
            
        }

        protected void DoAttack()
        {
            if (CanAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }
}
