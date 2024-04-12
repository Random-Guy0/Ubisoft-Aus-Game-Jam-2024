using System;
using System.Collections;
using System.Collections.Generic;
using Jam.Entities;
using Jam.Managers;
using UnityEngine;

namespace Jam.Entities.Bin
{
    public class Bin : Entity
    {
        [SerializeField] private AudioClip collectSound;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Trash.Trash trash) && !trash.Destroyed)
            {
                trash.Destroyed = true;
                SoundManager.Instance.PlaySound(collectSound, gameObject);
                PlayManager.Instance.AddScore();
                PlayManager.Instance.RemoveTrash();
                Destroy(trash.gameObject);
            }
        }
    }
}
