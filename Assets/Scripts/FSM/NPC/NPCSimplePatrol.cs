using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
using UnityEditor;

public class NPCSimplePatrol : MonoBehaviour
{
   //dictates whether the agent waits on each node

   [SerializeField] private bool _patrolWaiting;

   [SerializeField] float _totalWaitTime = 3f;

   [SerializeField] private float _switchProbability = 0.2f;
   [SerializeField] private List<Waypoint> _patrolPoints;

   [SerializeField] NavMeshAgent _navMeshAgent;
   private int _currentPatrolIndex;
   private bool _travelling;
   private bool _waiting = true;
   private bool _patrolForward;
   private float _waitTimer;
   
   
   private bool _chasing;
   public float sightRange, attackRange;
   public bool psightRange, pattackRange;
   public LayerMask Player;
   public Transform player;
   public int reachedDistance = 5;
   public void Awake()
   {
      player = GameObject.Find("Player").transform;
   }
   public void Update()
   {
      //if character is within area
      //do attacking state
      // set destination to near character (random angle and vecotr from character and set that as destination)
      // and start shooting (like waittimer but some kind of reload timer that if is not reloading then shoot()
      psightRange = Physics.CheckSphere(transform.position, sightRange, Player);
      pattackRange = Physics.CheckSphere(transform.position, attackRange, Player);
     
      if (psightRange && pattackRange) AttackPlayer();
   
      
      
      
      if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
      {
         _travelling = false;

         if (_patrolWaiting)
         {
            _waiting = true;
            _waitTimer = 0f;
         }
         else
         {
            ChangePatrolPoint();
            SetDestination();
         }
      }

      if (_waiting)
      {
         _waitTimer += Time.deltaTime;
         if (_waitTimer >= _totalWaitTime)
         {
            _waiting = false;

            ChangePatrolPoint();
            SetDestination();
         }
      }

      

      void SetDestination()
      {
         if (_patrolPoints != null)
         {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
         }
      }

      void ChangePatrolPoint()
      {
         if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
         {
            _patrolForward = !_patrolForward;
         }

         if (_patrolForward)
         {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
         }
         else
         {
            if (--_currentPatrolIndex < 0)
            {
               _currentPatrolIndex = _patrolPoints.Count - 1;
            }
         }
      }

     /* bool checkForPlayer()
      {
         
         return false;
         
      }*/
    

     void AttackPlayer()
     {
        Debug.Log("true");
        _navMeshAgent.destination = player.position;
     }
   }
}








