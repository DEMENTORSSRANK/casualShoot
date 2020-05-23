using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Bomb>())
            return;
        
        other.GetComponent<Bomb>().Boom();
        
        AttackRocket.Instance.DestroyRocket();
    }
}
