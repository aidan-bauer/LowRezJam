using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShoter : MonoBehaviour
{

    public KeyCode screenshotKey = KeyCode.Mouse1;

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(screenshotKey))
        {
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + DateTime.Now.ToString("yyyy-MM-dd hhmmssss") + ".png");
        }
        #endif
    }
}
