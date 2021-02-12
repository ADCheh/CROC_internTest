using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = true;
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
