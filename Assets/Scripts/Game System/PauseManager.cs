using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject TimmerPanel;

    public void PauseAction()
    {
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        TimmerPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeAction()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        TimmerPanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
