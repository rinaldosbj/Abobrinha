using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 0;

    public void gotHitted()
    {
        life-- ;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
