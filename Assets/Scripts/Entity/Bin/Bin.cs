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
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Trash.Trash trash))
            {
                PlayManager.Instance.AddTrash();
                Destroy(trash.gameObject);
            }
        }
    }
}
