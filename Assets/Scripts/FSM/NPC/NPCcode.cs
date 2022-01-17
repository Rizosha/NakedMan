using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
public class NPCcode : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private FiniteStateMachine _finiteStateMachine;
    [SerializeField] 
   NPCSimplePatrol[] _patrolPoints;
    public void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
    }
    public void Start()
    {
        
    }

    
    public void Update()
    {
        
        
    }

    public NPCSimplePatrol patrolPoints
    {
        get
        {
            return patrolPoints;
        }
    }
}




