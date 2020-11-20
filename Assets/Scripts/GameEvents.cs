using System;
using UnityEngine.Events;

public static class GameEvents
{
    // single event with no arguments
    public static UnityEvent OnPlayerDied = new UnityEvent();

    // single event taking a PickUp instance as an argument
    public static PickUpEvent OnPickUpCollected = new PickUpEvent();
    // definition for the PickUp event, deriving from UnityEvent
    public class PickUpEvent : UnityEvent<PickUp> { }
}
