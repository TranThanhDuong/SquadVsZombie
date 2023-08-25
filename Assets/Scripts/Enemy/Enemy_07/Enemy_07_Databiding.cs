using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_07_Databiding : MonoBehaviour
{
    public Animator animator;
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
    public float SpeedMove
    {
        set
        {
            animator.SetFloat(key_SpeedMove, value);
        }
    }
    private int key_Attack;
    private int key_Dead;
    private int key_SpeedMove;
    // Start is called before the first frame update
    void Start()
    {
        key_Attack = Animator.StringToHash("Attack");
        key_Dead = Animator.StringToHash("Dead");
        key_SpeedMove = Animator.StringToHash("SpeedMove");
    }
}
