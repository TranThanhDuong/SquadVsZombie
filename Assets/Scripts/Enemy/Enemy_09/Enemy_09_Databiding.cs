using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_09_Databiding : MonoBehaviour
{
    public Animator animator;
    public float SpeedMove
    {
        
        set
        {
            //Debug.LogError("move");
            animator.SetFloat(key_SpeedMove, value);
        }
        
    }
    public bool Wifi
    {
        set
        {
            if(value)
            {
                animator.SetTrigger(key_Wifi);
            }
            
        }
    }
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


    private int key_SpeedMove;
    private int key_Wifi;
    private int key_Dead;

    void Start()
    {
        key_SpeedMove = Animator.StringToHash("SpeedMove");
        key_Wifi = Animator.StringToHash("Wifi");
        key_Dead = Animator.StringToHash("Dead");
    }

}
