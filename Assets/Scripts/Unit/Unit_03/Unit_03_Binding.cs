using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_03_Binding : MonoBehaviour
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
    private int key_Attack;
    public bool Dead
    {
        set
        {
            if(value)
            {
                animator.SetTrigger(key_Dead);
            }
        }
    }
    private int key_Dead;
    public bool Reload
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(key_Reload);
            }
        }
    }
    private int key_Reload;
    void Start()
    {
        key_Attack = Animator.StringToHash("Attack");
        key_Dead = Animator.StringToHash("Dead");
        key_Reload = Animator.StringToHash("Reload");
    }
}
