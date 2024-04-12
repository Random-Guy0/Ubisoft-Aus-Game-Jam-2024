using System;
using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using Jam.Managers;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Jam.Entities.Trash
{
    public class Trash : Entity
    {
        [SerializeField] private float throwForce = 3f;
        [SerializeField] private Sprite[] trashSprites;
        
        private bool _grabbed = false;
        private Player.Player _grabber;

        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = LAYER_TRASH;
            PlayManager.Instance.AddTrash();
        }

        private void Start()
        {
            int randomSprite = Random.Range(0, trashSprites.Length);
            SpriteRenderer.sprite = trashSprites[randomSprite];
        }

        public void Grab(Transform grabber)
        {
            _grabbed = true;
            _grabber = grabber.GetComponent<Player.Player>();
            RigidBody.velocity = Vector2.zero;
            RigidBody.bodyType = RigidbodyType2D.Static;
        }

        public void Throw(Vector2 direction)
        {
            SpriteRenderer.enabled = true;
            RigidBody.bodyType = RigidbodyType2D.Dynamic;
            _grabbed = false;

            // Same direction and has some speed
            if(_grabber.RigidBody.velocity.normalized == direction && _grabber.RigidBody.velocity.magnitude >= 0.9f)
            {
                RigidBody.AddForce(direction * throwForce * 1.4f, ForceMode2D.Impulse);
            }
            else
            {
                RigidBody.AddForce(direction * throwForce, ForceMode2D.Impulse);

            }


        }

        public void HideBehindPlayer()
        {
            SpriteRenderer.enabled = false;
        }

        public void ShowInFrontOfPlayer()
        {
            SpriteRenderer.enabled = true;
        }

        private void Update()
        {
            if (_grabbed)
            {
                transform.position = _grabber.transform.position + (Vector3)_grabber.PlayerAttackHandler.AttackDirection * 0.47f;
            }
        }
    }
}
