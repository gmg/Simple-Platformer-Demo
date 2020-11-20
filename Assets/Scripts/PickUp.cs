using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int value = 5;

    public int Value { get => value; set => this.value = value; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameEvents.OnPickUpCollected?.Invoke(this);
        }
    }
}
