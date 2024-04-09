using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jam.Entities;

public class Notifier : MonoBehaviour
{
    // Start is called before the first frame update
    HealthBase healthSystem;
    void Start()
    {
        healthSystem = GetComponent<HealthBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            healthSystem.TakeDamage(1);
        }
    }
}


