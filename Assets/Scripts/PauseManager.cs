using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    //just in case we need it
    //public static PauseManager instance;

    public GameObject pauseMenu;
    static bool isPaused = false;

    public static bool IsPaused
    {
        get
        {
            return isPaused;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        SetTimeScale(isPaused);     //lock the cursor when level loads
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: localize the input to the player movement script
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            SetTimeScale(isPaused);
            SetPauseUI(isPaused);
        }
    }

    public void SetTimeScale(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;

        if (paused)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    //toggle UI
    public void SetPauseUI(bool paused)
    {
        pauseMenu.SetActive(IsPaused);
    }

    public void UnPauseViaUI()
    {
        isPaused = false;
        SetTimeScale(isPaused);
        SetPauseUI(isPaused);
    }

    public void EndofLevel()
    {
        isPaused = true;
        SetTimeScale(isPaused);
    }
}
