using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void QuitGame()
    {
        PlayerPrefs.SetInt("OldScore", 0);
        Application.Quit();
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("OldScore", 0);
        SceneManager.LoadScene(1);
    }
}
