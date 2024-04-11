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

        private void Start()
        {
            int randomSprite = Random.Range(0, trashSprites.Length);
            SpriteRenderer.sprite = trashSprites[randomSprite];
        }

        public void Grab(Transform grabber)
        {
            _grabbed = true;
            _grabber = grabber;
        }

        public void Throw(Vector2 direction)
        {
            _grabbed = false;
            RigidBody.AddForce(direction * throwForce);
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
