using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameController;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject confirmObject;

    void Start()
    {
        gameController = GameObject.Find("Game Controller");
    }

    public void PauseButton()
    {
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        restartButton.SetActive(true);
        gameController.GetComponent<GameController>().pauseMenuOpened = true;
    }

    public void ResumeButton()
    {
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        if(restartButton.activeSelf) { restartButton.SetActive(false); }
        if(confirmObject.activeSelf) { confirmObject.SetActive(false); }
        gameController.GetComponent<GameController>().pauseMenuOpened = false;
    }

    public void RestartButton()
    {
        confirmObject.SetActive(true);
        restartButton.SetActive(false);
    }

    public void ConfirmButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
