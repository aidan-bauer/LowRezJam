using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{

    public bool triggerOnce;
    bool triggered;
    public UnityEvent eventsToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                if (!triggered)
                    eventsToTrigger.Invoke();

                if (triggerOnce)
                    triggered = true;
            }
        }
    }
}
