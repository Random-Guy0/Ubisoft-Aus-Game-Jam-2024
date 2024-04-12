using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private void LateUpdate()
    {
        Vector2 position = transform.position;
        position = position.Clamp(minPosition, maxPosition);
        transform.position = position;
    }
}
