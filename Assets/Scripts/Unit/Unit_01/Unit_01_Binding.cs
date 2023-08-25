using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_01_Binding : MonoBehaviour
{
    public Animator animator;
    public bool Attack
    {
        set
        {
            if (value)
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
    
    private int key_Attack;
    private int key_Dead;
    // Start is called before the first frame update
    void Start()
    {
       
        key_Attack = Animator.StringToHash("Attack");
        key_Dead = Animator.StringToHash("Dead");
    }
}
