using System;
using UnityEngine;
using Jam.Entities.Trash;
using UnityEngine.InputSystem;

namespace Jam.Entities.Player
{
    public class PlayerTrashHandler : MonoBehaviour
    {
        [SerializeField] private float grabDistance = 2.5f;
        [SerializeField] private LayerMask grabLayerMask;
        
        private Player _entity;

        private bool hasGrabbedTrash = false;
        private Trash.Trash grabbedTrash;

        private void Awake()
        {
            _entity = GetComponent<Player>();
        }

        private void InteractWithTrash()
        {
            Vector2 direction = _entity.PlayerAttackHandler.AttackDirection;
            
            if (hasGrabbedTrash)
            {
                hasGrabbedTrash = false;
                grabbedTrash.Throw(direction);
            }
            else
            {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, grabDistance, grabLayerMask);
                #if UNITY_EDITOR
                Debug.DrawRay(transform.position, direction * grabDistance, Color.red, 0.5f);
                #endif
                if (raycast && raycast.transform.TryGetComponent<Trash.Trash>(out Trash.Trash trash))
                {
                    hasGrabbedTrash = true;
                    trash.Grab(transform);
                    grabbedTrash = trash;
                }
            }
        }

        public void OnInteractWithTrash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InteractWithTrash();
            }
        }
    }
}