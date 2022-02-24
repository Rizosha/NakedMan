using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using FSM;

public class NpcMove : MonoBehaviour
{
 [FormerlySerializedAs("_destination")] [SerializeField] private Transform destination;

 private NavMeshAgent _navMeshAgent;

 private void Start()
 {
  _navMeshAgent = this.GetComponent<NavMeshAgent>();
  if (_navMeshAgent == null)
  {
   Debug.LogError("the nav mesh component is not attatched to " +gameObject.name);
  }
  else
  {
   SetDestination();
  }
 }

 private void SetDestination()
 {
  if (destination != null)
  {
   Vector3 targetVector = destination.transform.position;
   _navMeshAgent.SetDestination(targetVector);
  }
 }
}
