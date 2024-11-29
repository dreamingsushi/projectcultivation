using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public void StartButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
