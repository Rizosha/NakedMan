using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
  NONE,
  ACTIVE,
  COMPLETED,
  TERMINATED,
}

public enum FSMStateType
{
  IDLE,
  PATROL,
};

public abstract class AbstractFSMState : ScriptableObject
{
  protected NavMeshAgent _navMeshAgent;
  protected NPCcode npCcode;
  protected FiniteStateMachine _fsm;
public ExecutionState ExecutionState { get; protected set; }
public bool EnteredState { get; protected set; }

public virtual void OnEnable()
{
  ExecutionState = ExecutionState.NONE;
}

public virtual bool EnterState()
{
  bool successNavMesh = true;
  bool successNPC = true;
  ExecutionState = ExecutionState.ACTIVE;
  
  // Does the NavMeshAgent exist?
   successNavMesh = (_navMeshAgent != null);
   
   
   //does the executing agent exist?
   successNPC = (npCcode != null);
   return successNavMesh & successNPC;
  }

  public abstract void UpdateState();
  
  public virtual bool ExitState()
  {
    ExecutionState = ExecutionState.COMPLETED;
    return true;
  }

  public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
  {
    if (navMeshAgent != null)
    {
      _navMeshAgent = navMeshAgent;
    }
  }

  public virtual void SetExecutingFSM(FiniteStateMachine fsm)
  {
    if (fsm != null)
    {
      _fsm = fsm;
    }
  }
  
  public virtual void SetExecutingNPC(NPCcode npCcode)
  {
    if (npCcode != null)
    {
      this.npCcode = npCcode;
    }
  }
}
