using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.Entities
{

    /// <summary>
    /// Base class for all interactable/interacting entities in the scene.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Entity : MonoBehaviour
    {
        public const int LAYER_PLAYER = 6;
        public const int LAYER_ENEMY = 7;
        public const int LAYER_ENTITY = 8;


        protected Rigidbody2D rigidBody;
        protected BoxCollider2D boxCollider;
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;


        public Rigidbody2D RigidBody{ get { return rigidBody; } set { rigidBody = value; } }
        public BoxCollider2D BoxCollider { get { return boxCollider; } set { boxCollider = value; } }
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } set { spriteRenderer = value; } }
        public Animator Animator { get { return animator; } set { animator = value; } }
        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            rigidBody.constraints |= RigidbodyConstraints2D.FreezeRotation;
            gameObject.layer = LAYER_ENTITY;


            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
    }

}


