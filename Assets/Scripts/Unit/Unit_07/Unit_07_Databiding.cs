using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_07_Databiding : MonoBehaviour
{
    public Animator animator;

    private int key_Dead;
    private int key_Reload;
    private int key_Attack;

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
    public bool Attack
    {
        set
        {
            animator.SetBool(key_Attack, value);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        key_Dead = Animator.StringToHash("Dead");
        key_Reload = Animator.StringToHash("Reload");
        key_Attack = Animator.StringToHash("Attack");
    }
}
