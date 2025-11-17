using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class menuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _selectionMenu;

    [Header("Scripts to deactivate on pause")]
    [SerializeField] private ShootScript player;

    [Header("First selected options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _startMenuFirst;
    [SerializeField] private GameObject _selectionMenuFirst;

    private bool isPaused;
    private void Start()
    {
        isPaused = false;
        _pauseMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        _startMenu.SetActive(true);
        _selectionMenu.SetActive(false);
    }
    private void Update()
    {
        if (ControllerInputManager.instance.MenuOpenCloseInput)
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        player.enabled = true;
        isPaused = false;
        Time.timeScale = 1f;
        CloseAllMenus();
    }

    private void CloseAllMenus()
    {
        _pauseMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

    }

    public void Pause()
    {
        player.enabled = false;
        isPaused = true;
        Time.timeScale = 0f;
        OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        _pauseMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenBountyMenu()
    {
        _selectionMenu.SetActive(true);
        _startMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_selectionMenuFirst);
    }
    public void CloseBountyMenu()
    {
        _selectionMenu.SetActive(false);
        _startMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
}
