using UnityEngine;

public class IdleState: StateInterface
{
    CharacterController controller;
    public void EnterState()
    {
        Debug.Log("Entering Idle State");
    }

    public void UpdateState()
    {
        
        Debug.Log("Updating Idle State");
    }

    public void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }
}