using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField] 
        private AbstractFSMState _startingState;
        
        private AbstractFSMState _currentState;
        [SerializeField]
        List<AbstractFSMState> _validStates;
        private Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
            NPC npc = this.GetComponent<NPC>();
            foreach (AbstractFSMState state in _validStates)
            {
                    state.SetExecutingFSM(this);
                    state.SetExecutingNPC(npc);
                    state.SetNavMeshAgent(navMeshAgent);
            }
            
        }

        public void Start()
        {
            EnterState(_startingState);
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        public void EnterState(AbstractFSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }

            _currentState = nextState;
            _currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            
        }
    }

    public class NPC : NPCcode
    {
    }
}