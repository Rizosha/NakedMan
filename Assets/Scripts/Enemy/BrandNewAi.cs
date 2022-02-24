using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BrandNewAi : MonoBehaviour
{
    // Patrol/Idle State
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

    // attack range
    public float sightRange, attackRange;
    public float bonusSightRange, bonusAttackRange;
    private bool isSightRange, isAttackRange;
    
    public Transform player;
    public LayerMask Player;

    public Transform barrel;
   
    //shoot interval
    private float shootTime;
    [SerializeField] public float reloadTime;
    [SerializeField] public float bReloadTime;
    public bool reloading = true;
    
    //health and rage health 
    public int health = 100;
    public int currentHealth;
    public int rageHealth;

    //key spawn
    private GameObject keyS;

    public HealthBar healthBar;

    // declaring the states that we are going to use
    private enum State
    {
        Patrol,
        Chase,
        Attack,
        LastStand
    }

    private void Start()
    {
        // health bar being set
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        keyS = GameObject.Find("KeySpawn");
    }

    private void Awake()
    {
        //starts in the patrol state
        state = State.Patrol;
    }

    private void Update()
    {
        // Main part of the code is this switch. It works off the case state above running certain methods below
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
        
        // last stand condition
        if (currentHealth <= rageHealth)
        {
            state = State.LastStand;
        }

        void Idling()
        {
            // using a wait timer to set the destination
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

        //looks through the patrol points we set and sets the destination while looking at the player. 
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

        //changes the patrol point based on a random probability
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
        
        //uses check spheres to see if the player is in range. This can change the state into chase or attack
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

        //chases the player by setting destination to player and then reducing the AI speed to 0 to maintain a distance
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

        // attacks player by using the timer from above and setting the bullet to active. Also adds a force to bullet. 
        void AttackPlayer()
        {
            
            if (shootTime < reloadTime)
            {
                reloading = true;
            }

            if (reloading)
            {
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
                        rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
                    }
                    reloading = false;
                    shootTime = 0;
                }
            }
        }
        
        // kills enemy and spawns key
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            keyS.GetComponent<SpawnKey>().enemiesleft -= 1;
        }
    }
    // later added beserk mode. Ramps up the difficulty by increasing attack rate 
    private void BeserkPlayer()
    {
        if (shootTime < reloadTime)
        {
            reloading = true;
        }

        if (reloading)
        {
            shootTime += Time.deltaTime;
            if (shootTime > bReloadTime)
            {
                GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    bullet.transform.position = barrel.transform.position;
                    
                    bullet.SetActive(true);
                    rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
                }
                reloading = false;
                shootTime = 0;
            }
        }
    }
    
    // changes AI beserk attack range and sight range
    private void playerStandCheck()
    {
        var position = transform.position;
        isSightRange = Physics.CheckSphere(position, bonusSightRange, Player);
        isAttackRange = Physics.CheckSphere(position, bonusAttackRange, Player);
    }

    // draws attack range gizmo
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

    //  updates health bar 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    //takes damage
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("pbullet"))
        {
            TakeDamage(10);
            col.gameObject.SetActive(false);
        }
    }
}