using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 0;
    private Animator animator;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    public void gotHitted()
    {
        life-- ;
        if (life <= 0)
        {
            animator.Play("Die");
        }
    }

    public void Die(){
        Destroy(gameObject);
    }
}
