using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startscreen : MonoBehaviour
{
    public GameObject tutorialscreen;

    private void Start()
    {
        tutorialscreen.SetActive(false);
    }
    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }
    public void quitgame()
    {
        Application.Quit();
    }

    public void tutorialbutton()
    {
        tutorialscreen.SetActive(true);
    }
    public void closebutton()
    {
        tutorialscreen.SetActive(false);
    }
}
