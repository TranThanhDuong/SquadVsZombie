using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit_08_AttackState : FSMState
{
        [NonSerialized]
        public Unit_08_Control parent;
        private float timeAttack;
        public override void OnEnter()
        {
            timeAttack =2;
             parent.databiding.Attack = true;
            parent.weapon.gameObject.SetActive(true);
            parent.weapon.Fire(true);
    }

        public override void FixedUpdate()
        {
            
            timeAttack -= Time.deltaTime;
            if (timeAttack <= 0)
            {
                parent.GotoState(parent.idleState, 5f);
            }
        }
    public override void OnExit()
    {
        parent.weapon.Fire(false);
        parent.databiding.Attack = false;
        parent.weapon.gameObject.SetActive(false);
    }

}
