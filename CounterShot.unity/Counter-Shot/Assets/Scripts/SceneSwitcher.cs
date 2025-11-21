using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
    [SerializeField] private GameObject _gunshotImage;
    [SerializeField] private AudioSource gunshotSound;
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void TrainingLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void Sundance()
    {
        _gunshotImage.SetActive(true);
        gunshotSound.Play();
        waitForSecconds();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    private IEnumerator waitForSecconds()
    {
        yield return new WaitForSeconds(1);
    }
}
