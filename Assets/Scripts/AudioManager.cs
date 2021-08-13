using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float pauseVolume = 0.4f;
    AudioSource source;

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused)
        {
            source.volume = 1f;
        } else
        {
            source.volume = pauseVolume;
        }
    }
}
