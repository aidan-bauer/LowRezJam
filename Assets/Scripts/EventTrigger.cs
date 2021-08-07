using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{

    public UnityEvent eventsToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                eventsToTrigger.Invoke();
            }
        }
    }
}
