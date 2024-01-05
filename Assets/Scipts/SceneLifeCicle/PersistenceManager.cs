using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager shared;

    public List<string> dontDestroyPreviousSceneList;

    public void ResetData(bool resetDontDestroy = false)
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Life", 3);
        PlayerPrefs.SetInt("Jumps",1);
        PlayerPrefs.SetInt("hasPowerUp",0);
        PlayerPrefs.SetInt("PreviousScore",0);
        PlayerPrefs.SetInt("Checkpointed",0);
        if (resetDontDestroy)
        {
            DontDestroy.shared.sceneList = new List<string>{"Manager"};
        }
    }

    private void UpdateDontDestroyPrevious(){
        dontDestroyPreviousSceneList = new List<string>();
        foreach (string name in DontDestroy.shared.sceneList)
        {
            if (CheckIfMustPersist(name) || !GameObject.Find(name).GetComponent<SpriteRenderer>().enabled)
            {
                dontDestroyPreviousSceneList.Add(name.ToString());
                Debug.Log($"updated previous -> {name}");
            }
        }
    }

    public void PrepareDontDestroyPreviousSceneList(){
        dontDestroyPreviousSceneList = new List<string>();
        foreach (string name in DontDestroy.shared.sceneList)
        {
            if (CheckIfMustPersist(name))
            {
                dontDestroyPreviousSceneList.Add(name.ToString());
                Debug.Log($"updated previous -> {name}");
            }
        }
    }

    public bool CheckIfMustPersist(string name){
        GameObject gameObjectFromName = GameObject.Find(name);
        if (gameObjectFromName == null){
            return false;
        }
        return gameObjectFromName.CompareTag("Undestroyable") || gameObjectFromName.CompareTag("Life") || gameObjectFromName.CompareTag("Power") || gameObjectFromName.CompareTag("DoubleJump");
    }

    private void Start() {
        shared = this;
        dontDestroyPreviousSceneList = new List<string>{"Manager"};
    }

    // Player Movement
    public int jumpQuantity(){
        return PlayerPrefs.GetInt("Jumps");
    }
    public void JumpUP(){
        PlayerPrefs.SetInt("Jumps",jumpQuantity() + 1);
    }
    public bool hasPowerUp(){
        return PlayerPrefs.GetInt("hasPowerUp") == 1;
    }

    public void GotPowerUp(){
        PlayerPrefs.SetInt("hasPowerUp", 1);
    }


    // Collectables
    public int lifes(){
        return PlayerPrefs.GetInt("Life");
    }

    public void LifeUP(){
        PlayerPrefs.SetInt("Life", lifes()+1);
    }
    
    public void LifeDOWN(){
        PlayerPrefs.SetInt("Life", lifes()-1);
    }

    public void UpdateScore(){
        PlayerPrefs.SetInt("Score", previousScore());
    }

    public int score(){
        return PlayerPrefs.GetInt("Score");
    }

    public void ScoreUP(){
        PlayerPrefs.SetInt("Score", score()+1);
    }

    // Checkpoint
    public int previousScore(){
        return PlayerPrefs.GetInt("PreviousScore");
    }

    private void UpdatePreviousScore(){
        PlayerPrefs.SetInt("PreviousScore", score());
    }

    public bool willSpawnOnCheckpoint(){
        return PlayerPrefs.GetInt("Checkpointed") == 1;
    }

    public Vector2 spawnpoint(){
        return new Vector2(PlayerPrefs.GetFloat("SpawnpointX"),PlayerPrefs.GetFloat("SpawnpointY"));
    }

    public void RestartToNextLevel(){
        DontDestroy.shared.sceneList = new List<string>{"Manager"};
        UpdateDontDestroyPrevious();
        UpdatePreviousScore();
        PlayerPrefs.SetInt("Checkpointed",0);
    }

    public void Checkpointed(Vector2 spawnpoint){
        PlayerPrefs.SetInt("Checkpointed", 1);
        PlayerPrefs.SetFloat("SpawnpointX", spawnpoint.x);
        PlayerPrefs.SetFloat("SpawnpointY", spawnpoint.y);
        UpdatePreviousScore();
        UpdateDontDestroyPrevious();
    }
}
