using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_02_Databinding : MonoBehaviour
{
    public Animator anim;
    public bool Attack
    {
        set
        {
            if(value)
            {
                anim.SetTrigger(key_Attack);
            }
        }
    }
    public bool Dead
    {
        set
        {
            if(value)
            {
                anim.SetTrigger(key_Dead);
            }
        }
    }
    private int key_Attack;
    private int key_Dead;

    void Start()
    {
        key_Attack = Animator.StringToHash("Attack");
        key_Dead = Animator.StringToHash("Dead");
    }
}
