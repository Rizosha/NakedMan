using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity=FSM/States/Patrol", order = 2)]
public class PatrolState : AbstractFSMState
{
    NPCSimplePatrol[] _patrolPoints;
    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        _patrolPointIndex = -1;
        
    }

    public override bool EnterState()
    { 
        EnteredState = false;
        if (base.EnterState())
        {
            //Grab and store the patrol points
            //_patrolPoints = NPCSimplePatrol._patrolPoints;
            
           
            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                Debug.LogError("PatrolState: Failed to grab patrol points from the NPC");
               
            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }
                
                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
            }

            
        }

        return EnteredState;

    }

    public override void UpdateState()
    {
        //TODO: Need to make sure we've successfully entered the state
        if(EnteredState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position,
               _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
        
    }

    private void SetDestination(NPCSimplePatrol destination)
    {
        if (_navMeshAgent != null && destination != null)
        {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
