using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem : MonoBehaviour
{
    public FSMState currentState;
    public List<FSMState> states = new List<FSMState>();
    public void AddState(FSMState newState)
    {
        states.Add(newState);
        if(states.Count==1)
        {
            currentState = newState;
            currentState.OnEnter();
        }
    }
    public void GotoState(FSMState newState)
    {
        if (currentState!=null)
        {

            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }
    public void GotoState(FSMState newState, object data)
    {
        if (currentState != null)
        {

            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter(data);
    }

    // Update is called once per frame
    private void Update()
    {
        SystemUpdate();
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void FixedUpdate()
    {
        SystemFixedUpdate();
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    private void LateUpdate()
    {
        SystemLateUpdate();
        if(currentState!=null)
        {
            currentState.LateUpdate();
        }
    }
    public  virtual void SystemUpdate()
    {

    }

    public virtual void SystemFixedUpdate()
    {

    }

    public virtual void SystemLateUpdate()
    {

    }
}
