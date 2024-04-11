using System;
using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Jam.Entities.Trash
{
    public class Trash : Entity
    {
        [SerializeField] private float throwForce = 5f;
        [SerializeField] private Sprite[] trashSprites;
        
        private bool _grabbed = false;
        private Transform _grabber;

        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = LAYER_TRASH;
        }

        private void Start()
        {
            int randomSprite = Random.Range(0, trashSprites.Length);
            SpriteRenderer.sprite = trashSprites[randomSprite];
        }

        public void Grab(Transform grabber)
        {
            _grabbed = true;
            _grabber = grabber;
            RigidBody.velocity = Vector2.zero;
            RigidBody.bodyType = RigidbodyType2D.Static;
        }

        public void Throw(Vector2 direction)
        {
            RigidBody.bodyType = RigidbodyType2D.Dynamic;
            _grabbed = false;
            RigidBody.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }

        public void HideBehindPlayer()
        {
            SpriteRenderer.sortingOrder = -1;
        }

        public void ShowInFrontOfPlayer()
        {
            SpriteRenderer.sortingOrder = 1;
        }

        private void Update()
        {
            if (_grabbed)
            {
                transform.position = _grabber.transform.position;
            }
        }
    }
}
