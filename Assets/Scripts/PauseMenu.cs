using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject CanvasPauseMenu;

    public void StartMenu() {
        CanvasPauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        CanvasPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame() {
        Application.Quit();
        Time.timeScale = 1;
    }
}