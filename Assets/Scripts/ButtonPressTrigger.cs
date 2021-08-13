using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPressTrigger : MonoBehaviour
{
    public UnityEvent eventsToTrigger;
    bool isPlayerInTrigger = false;

    private void Update()
    {
        if (!PauseManager.IsPaused && isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                eventsToTrigger.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                isPlayerInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                isPlayerInTrigger = false;
            }
        }
    }
}
