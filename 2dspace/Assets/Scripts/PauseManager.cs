using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static GameObject[] pauseUI;
    private static bool enabledPause;
    private static int pauseValue;
    private static bool showPause;
    public static bool allowPause;

    // Use this for initialization
    void Start()
    {
        pauseValue = 1;
        //this boolean shows or hides the PauseUI
        showPause = false;
        //enabledPause is TRUE when the "P" key is pressed
        enabledPause = false;
        //allow pause in loader screen
        allowPause = false;
        pauseUI = GameObject.FindGameObjectsWithTag("Pause");
        hidePauseUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            enabledPause = true;
            if (pauseValue == 1)
            {
                pauseValue = 0;
                showPause = true;
                Debug.Log("if");
            }
            else
            {
                pauseValue = 1;
                showPause = false;
                Debug.Log("else if");
            }
        }

        if(allowPause && enabledPause) 
        {
            if (showPause)
            {
                showPauseUI();
            }
            else
            {
                hidePauseUI();
            }
            enabledPause = false;
        }

        setTimeScale(pauseValue);
    }

    private static void setTimeScale(int timeValue) {
        Time.timeScale = timeValue;
    }

    public static void AllowPause(bool value) {
        allowPause = value;
    }

    private static void showPauseUI()
    {
        setActivePauseUI(true);
    }

    private static void hidePauseUI()
    {
        setActivePauseUI(false);
    }

    //Active/inactive the pause when the loader appears.
    private static void setActivePauseUI(bool value)
    {
        foreach (GameObject go in pauseUI)
        {
            go.SetActive(value);
        }
    }

    //Hide the PauseUI
    public static void pauseButtonAction() {
        //showPause = false;
        pauseValue = 1;
        setTimeScale(pauseValue);
        hidePauseUI();
    }
}



