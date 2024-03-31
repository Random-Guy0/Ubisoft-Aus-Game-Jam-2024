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
        private Rigidbody2D rigidBody;
        private BoxCollider2D boxCollider;
        private SpriteRenderer spriteRenderer;
        private Animator animator;


        public Rigidbody2D RigidBody{ get { return rigidBody; } set { rigidBody = value; } }
        public BoxCollider2D BoxCollider { get { return boxCollider; } set { boxCollider = value; } }
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } set { spriteRenderer = value; } }
        public Animator Animator { get { return animator; } set { animator = value; } }
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
    }

}


