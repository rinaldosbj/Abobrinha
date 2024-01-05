using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void QuitGame()
    {
        PersistenceManager.shared.ResetData();
        Application.Quit();
    }
    public void StartGame()
    {
        PersistenceManager.shared.ResetData(resetDontDestroy: true);
        SceneManager.LoadScene(1);
    }
}
