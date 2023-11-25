using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void QuitGame()
    {
        ResetData();
        Application.Quit();
    }
    public void StartGame()
    {
        ResetData();
        SceneManager.LoadScene(1);
    }

    private void ResetData()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("OldScore", 0);
        PlayerPrefs.SetInt("Life", 3);
        DontDestroy.dontDestroy.sceneList = new List<string>();
    }
}
