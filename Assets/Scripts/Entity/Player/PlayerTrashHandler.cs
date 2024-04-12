using System;
using System.Collections;
using UnityEngine;
using Jam.Entities.Trash;
using UnityEngine.InputSystem;

namespace Jam.Entities.Player
{
    public class PlayerTrashHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 grabSize = Vector2.one * 0.5f;
        [SerializeField] private float grabDistance = 1f;
        [SerializeField] private LayerMask grabLayerMask;
        
        private Player _entity;

        public bool hasGrabbedTrash = false;
        public Trash.Trash grabbedTrash;

        private void Awake()
        {
            _entity = GetComponent<Player>();
        }

        private void InteractWithTrash()
        {
            _entity.PlayerAttackHandler.CanAttack = false;
            Vector2 direction = _entity.PlayerAttackHandler.AttackDirection;
            _entity.Animator.SetFloat("AttackSpeed", 5f);
            _entity.Animator.SetBool("Attacking", true);
            
            if (hasGrabbedTrash)
            {
                hasGrabbedTrash = false;
                grabbedTrash.Throw(direction);
                grabbedTrash = null;
            }
            else
            {
                Vector2 origin = (Vector2)transform.position + direction * grabDistance;
                RaycastHit2D raycast = Physics2D.BoxCast(origin, grabSize, 0f, direction, grabDistance, grabLayerMask);
                #if UNITY_EDITOR
                Debug.DrawLine(new Vector3(origin.x + grabSize.x * 0.5f, origin.y + grabSize.y * 0.5f), new Vector3(origin.x - grabSize.x * 0.5f, origin.y + grabSize.y * 0.5f), Color.red);
                Debug.DrawLine(new Vector3(origin.x + grabSize.x * 0.5f, origin.y + grabSize.y * 0.5f), new Vector3(origin.x + grabSize.x * 0.5f, origin.y - grabSize.y * 0.5f), Color.red);
                Debug.DrawLine(new Vector3(origin.x + grabSize.x * 0.5f, origin.y - grabSize.y * 0.5f), new Vector3(origin.x - grabSize.x * 0.5f, origin.y - grabSize.y * 0.5f), Color.red);
                Debug.DrawLine(new Vector3(origin.x - grabSize.x * 0.5f, origin.y + grabSize.y * 0.5f), new Vector3(origin.x - grabSize.x * 0.5f, origin.y - grabSize.y * 0.5f), Color.red); 
                #endif
                if (raycast && raycast.transform.TryGetComponent<Trash.Trash>(out Trash.Trash trash))
                {
                    hasGrabbedTrash = true;
                    trash.Grab(transform);
                    grabbedTrash = trash;
                }
            }

            StartCoroutine(StopAttackAnimation());
        }

        private IEnumerator StopAttackAnimation()
        {
            yield return new WaitForSeconds(0.1f);
            _entity.Animator.SetBool("Attacking", false);
            _entity.PlayerAttackHandler.CanAttack = true;
        }

        public void OnInteractWithTrash(InputAction.CallbackContext context)
        {
            if (context.performed && _entity.PlayerAttackHandler.CanAttack)
            {
                InteractWithTrash();
            }
        }

        private void Update()
        {
            if (grabbedTrash != null)
            {
                Vector2 direction = _entity.PlayerAttackHandler.AttackDirection;

                if (direction == Vector2.up)
                {
                    grabbedTrash.HideBehindPlayer();
                }
                else
                {
                    grabbedTrash.ShowInFrontOfPlayer();
                }
            }
        }
    }
}