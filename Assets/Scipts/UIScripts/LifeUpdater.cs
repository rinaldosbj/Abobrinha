using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUpdater : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        text.text = PersistenceManager.shared.lifes().ToString();
    }
}
