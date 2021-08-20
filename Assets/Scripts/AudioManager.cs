using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float pauseVolume = 0.4f;
    [SerializeField] AudioSource source;
    float unpausedVolumne;

    private void Awake()
    {
        //source = GetComponentInChildren<AudioSource>();

        unpausedVolumne = source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused)
        {
            source.volume = unpausedVolumne;
        } else
        {
            source.volume = pauseVolume;
        }
    }
}
