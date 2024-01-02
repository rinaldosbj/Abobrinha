using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy dontDestroy;
    public List<string> sceneList;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(dontDestroy == null)
        {
            dontDestroy = this;
            dontDestroy.sceneList = new List<string>();
        }

        if (dontDestroy.sceneList.Count == 0)
        {
            dontDestroy.sceneList.Add($"{gameObject.name}");
        }
        else if (dontDestroy.sceneList.Contains($"{gameObject.name}"))
        {
            Destroy(gameObject);
        }
        else
        {
            dontDestroy.sceneList.Add($"{gameObject.name}");
        }

    }
}
