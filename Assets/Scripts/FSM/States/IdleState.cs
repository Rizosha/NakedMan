using UnityEngine;
using System;
using System.Collections.Generic;
using  System.Linq;
using System.Text;


namespace FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "Unity=FSM/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("IdleState");
            EnteredState = true;
            return EnteredState;
        }
        
        public override void UpdateState()
        {
            Debug.Log("updating");
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exit Idle");
            return true;
        }
    }
}