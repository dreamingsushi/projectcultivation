using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
