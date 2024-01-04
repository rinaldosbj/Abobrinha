using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void QuitGame()
    {
        PersistenceManager.shared.resetData();
        Application.Quit();
    }
    public void StartGame()
    {
        PersistenceManager.shared.resetData(resetDontDestroy: true);
        SceneManager.LoadScene(1);
    }
}
