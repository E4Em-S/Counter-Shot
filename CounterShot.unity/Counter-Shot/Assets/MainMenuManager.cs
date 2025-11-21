using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _selectionMenu;



    [Header("First selected options")]
    [SerializeField] private GameObject _startMenuFirst;
    [SerializeField] private GameObject _selectionMenuFirst;



    private bool isPaused;
    private void Start()
    {
        if(SceneManager.GetActiveScene().name== "MainMenuScene")
        {
         _startMenu.SetActive(true);
        _selectionMenu.SetActive(false);
        }

    }
    private void Update()
    {
       
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
        EventSystem.current.SetSelectedGameObject(_startMenuFirst);
    }
}
