using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
