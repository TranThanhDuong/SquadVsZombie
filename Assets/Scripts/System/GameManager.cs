using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    EnemyAlive = 1,
    UseMoney,
    TimeDone,
    UnitDead,

}
public enum RewardType
{
    Gold = 1,
    Exp,
    Energy,
    Gem,
    Card,
}

public class GameManager : Singleton<GameManager>
{
    public override void OnAwake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
