using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace SpaceInvader
{
    public class EnemyShip : Ship
{
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _Player;
    [SerializeField] private LayerMask _GroundMask, PlayerMask;
    [SerializeField] private GameObject _projectile;
    
    
    [SerializeField] private Vector3 flyPoint;
    [SerializeField] private bool flyPointSet;
    [SerializeField] private float flyPointRange;

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;
    [SerializeField] private GameObject gunTip;

    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MovmentSpeed;
        _Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check for player if in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);
        
        
        if(!playerInSightRange && !playerInAttackRange) Roaming();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    public void takeDamage(int amount)
    {
        Debug.Log("Enemy Took damage");
        TakeDamage(amount);
    }
    
    private void Roaming()
    {
        if (!flyPointSet)
        {
            SearchFlyPoint();
        }
        if(flyPointSet)
        {
            _agent.SetDestination(flyPoint);
        }

        Vector3 dist = transform.position - flyPoint;
        
        
        if (dist.magnitude <= 2f)
        {
            flyPointSet = false;
        }
    }
    private void SearchFlyPoint()
    {
        flyPoint = new Vector3(
            transform.position.x + Random.Range(-flyPointRange, flyPointRange),
            transform.position.y,
            transform.position.x + Random.Range(-flyPointRange, flyPointRange)
        );

        if (Physics.Raycast(flyPoint, -transform.up, 2f, _GroundMask))
        {
            flyPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        _agent.SetDestination(_Player.position);
    }
    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_Player);
        
        if (!alreadyAttacked)
        {
            
            // Attack stuff
            Rigidbody rb = Instantiate(_projectile, gunTip.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(RestAttack), timeBetweenAttacks);
        }
    }
    private void RestAttack()
    {
        alreadyAttacked = false;
    }
}

}
