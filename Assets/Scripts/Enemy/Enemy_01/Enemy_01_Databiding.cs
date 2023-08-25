using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Databiding : MonoBehaviour
{
    public Animator animator;
    public float SpeedMove
    {
        set
        {
            animator.SetFloat(key_SpeedMove, value);
        }
    }
    public bool Attack
    {
        set
        {
            if(value)
            {
                animator.SetTrigger(key_Attack);
            }
        }
    }
    public bool Dead
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(key_Dead);
            }
        }
    }
    private int key_SpeedMove;
    private int key_Attack;
    private int key_Dead;
    // Start is called before the first frame update
    void Start()
    {
        key_SpeedMove = Animator.StringToHash("SpeedMove");
        key_Attack = Animator.StringToHash("Attack");
        key_Dead = Animator.StringToHash("Dead");
    }

  
}
