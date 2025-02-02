﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.OnPickUpCollected?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
