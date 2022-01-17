using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BrandNewAi : MonoBehaviour
{
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

    private State state;

    public float sightRange, attackRange;
    public float bonusSightRange, bonusAttackRange;

    private bool isSightRange, isAttackRange;
    
    public Transform player;
    public LayerMask Player;

    public Transform barrel;
   
    private float shootTime;
    [SerializeField] public float reloadTime;
    [SerializeField] public float bReloadTime;
    public bool reloading = true;
    public int health = 100;
    public int currentHealth;
    public int rageHealth;

    private GameObject keyS;

    public HealthBar healthBar;

    private enum State
    {
        Patrol,
        Chase,
        Attack,
        LastStand
    }

    private void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        keyS = GameObject.Find("KeySpawn");
    }

    private void Awake()
    {
        state = State.Patrol;
    }

    private void Update()
    {
        switch (state)
        {
            default:

            case State.Patrol:
                playerCheck();
                Idling();
                break;
            case State.Chase:
                playerCheck();
                ChasePlayer();
                break;
            case State.Attack:
                playerCheck();
                ChasePlayer();
                AttackPlayer();
               
                break;
            case State.LastStand:
                playerStandCheck();
                ChasePlayer();
                BeserkPlayer();
               break;
        }

        if (currentHealth <= rageHealth)
        {
            state = State.LastStand;
        }

        void Idling()
        {
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
        }

        void SetDestination()
        {
            if (_patrolPoints != null)
            {
                Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
                _navMeshAgent.SetDestination(targetVector);
                transform.LookAt(targetVector);
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

        void playerCheck()
        {
            var position = transform.position;
            isSightRange = Physics.CheckSphere(position, sightRange, Player);
            isAttackRange = Physics.CheckSphere(position, attackRange, Player);

            if (!isSightRange && !isAttackRange)
            {
                state = State.Patrol;
            }

            if (isSightRange && !isAttackRange)
            {
                state = State.Chase;

            }

            if (isSightRange && isAttackRange)
            {
                state = State.Attack;
            }
        }

        void ChasePlayer()
        {
            transform.LookAt(player);

            var transformEulerAngles = transform.eulerAngles;
            transformEulerAngles.y = 0;
            _navMeshAgent.destination = player.position;
            if (_navMeshAgent.remainingDistance < 1)
            {
                _navMeshAgent.speed = 0f;
            }
            else
            {
                _navMeshAgent.speed = 0.96f;
            }
        }

      

        void AttackPlayer()
        {
            
            if (shootTime < reloadTime)
            {
                reloading = true;
            }

            if (reloading)
            {
                // Debug.Log("reloading");
                shootTime += Time.deltaTime;
                if (shootTime > reloadTime)
                {
                    GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
                    if (bullet != null)
                    {
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        bullet.transform.position = barrel.transform.position;
                        bullet.transform.rotation = barrel.transform.rotation;

                        bullet.SetActive(true);
                        // bullet.transform.LookAt(player);
                        rb.AddForce(transform.forward * 8f, ForceMode.Impulse);



                    }
                    //Debug.Log("reloaded");
                    // Rigidbody rb = Instantiate(bullet, barrel.position, Quaternion.identity).GetComponent<Rigidbody>();
                    // rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

                    reloading = false;
                    shootTime = 0;
                }
            }
        }


        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            keyS.GetComponent<SpawnKey>().enemiesleft -= 1;
        }
    }

    private void BeserkPlayer()
    {
        if (shootTime < reloadTime)
        {
            reloading = true;
        }

        if (reloading)
        {
            // Debug.Log("reloading");
            shootTime += Time.deltaTime;
            if (shootTime > bReloadTime)
            {
                GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    bullet.transform.position = barrel.transform.position;
                   

                    bullet.SetActive(true);
                    // bullet.transform.LookAt(player);
                    rb.AddForce(transform.forward * 8f, ForceMode.Impulse);



                }
                //Debug.Log("reloaded");
                // Rigidbody rb = Instantiate(bullet, barrel.position, Quaternion.identity).GetComponent<Rigidbody>();
                // rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

                reloading = false;
                shootTime = 0;
            }
        }
    }

    private void playerStandCheck()
    {
        var position = transform.position;
        isSightRange = Physics.CheckSphere(position, bonusSightRange, Player);
        isAttackRange = Physics.CheckSphere(position, bonusAttackRange, Player);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, bonusSightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, bonusAttackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("pbullet"))
        {
            TakeDamage(10);
            col.gameObject.SetActive(false);

        }
    }
    
    

    

}