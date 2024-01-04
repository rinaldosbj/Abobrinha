using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager shared;

    public List<string> dontDestroyPreviousSceneList;

    public void resetData(bool resetDontDestroy = false)
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Life", 3);
        PlayerPrefs.SetInt("Jumps",1);
        PlayerPrefs.SetInt("hasPowerUp",0);
        PlayerPrefs.SetInt("PreviousScore",0);
        if (resetDontDestroy)
        {
            DontDestroy.shared.sceneList = new List<string>();
        }
    }

    public void updateDontDestroyPrevious(){
        dontDestroyPreviousSceneList = new List<string>();
        foreach (string name in DontDestroy.shared.sceneList)
        {
            if (name == "Manager" || name == "Checkpoint" || !GameObject.Find(name).GetComponent<SpriteRenderer>().enabled)
            {
                dontDestroyPreviousSceneList.Add(name.ToString());
                Debug.Log($"updated previous -> {name}");
            }
        }
    }

    private void Start() {
        shared = this;
        dontDestroyPreviousSceneList = new List<string>{"Manager"};
    }

    // Player Movement
    public int jumpQuantity(){
        return PlayerPrefs.GetInt("Jumps");
    }
    public void jumpUP(){
        PlayerPrefs.SetInt("Jumps",jumpQuantity() + 1);
    }
    public bool hasPowerUp(){
        return PlayerPrefs.GetInt("hasPowerUp") == 1;
    }

    public void gotPowerUp(){
        PlayerPrefs.SetInt("hasPowerUp", 1);
    }


    // Collectables
    public int lifes(){
        return PlayerPrefs.GetInt("Life");
    }

    public void lifeUP(){
        PlayerPrefs.SetInt("Life", lifes()+1);
    }
    
    public void lifeDOWN(){
        PlayerPrefs.SetInt("Life", lifes()-1);
    }

    public void updateScore(){
        PlayerPrefs.SetInt("Score", previousScore());
    }

    public int score(){
        return PlayerPrefs.GetInt("Score");
    }

    public void scoreUP(){
        PlayerPrefs.SetInt("Score", score()+1);
    }

    // Checkpoint
    public int previousScore(){
        return PlayerPrefs.GetInt("PreviousScore");
    }

    public void updatePreviousScore(){
        PlayerPrefs.SetInt("PreviousScore", score());
    }

}
