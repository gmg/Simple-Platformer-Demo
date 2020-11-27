using UnityEngine.Events;

public static class GameEvents
{
    // single event with no arguments
    public static UnityEvent OnPlayerDied = new UnityEvent();

    // single event taking a PickUp instance as an argument
    public static PickUpEvent OnPickUpCollected = new PickUpEvent();
    //public static PickUpEvent OnPickUpSpawned = new PickUpEvent();

    public static IntEvent OnScoreChanged = new IntEvent();

    // definition for the PickUp event, deriving from UnityEvent
    public class PickUpEvent : UnityEvent<PickUp> { }
    public class IntEvent : UnityEvent<int> { }
}
