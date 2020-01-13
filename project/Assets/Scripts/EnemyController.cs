using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float randomRange = 0f;
    private int stuckTime = 0;
    public GameManagerLogic manager;
    Transform target;
    NavMeshAgent agent;
    Rigidbody rb;
    Vector3 randomTarget;

    public enum EnemyState {idle, chase};
    public EnemyState state = EnemyState.idle;
    public float distance;
    public bool wandering = false;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    } 

    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        CheckState();        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.StageLost();
        }
    }

    /// <summary>
    ///  Returns a random target position that enemy must move to before detecting the player
    /// </summary>
    /// <returns></returns>
    Vector3 RandomTarget()
    {
        Vector3 position = new Vector3(Random.Range(-randomRange, randomRange), 0f, Random.Range(-randomRange, randomRange));
        position += transform.position;
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position.y = hit.point.y;
        }
        
        return position;
    }

    /// <summary>
    /// Enemy starts wandering;
    /// </summary>
    void Wander()
    {
        if (wandering)
        {
            return;
        }
        else
        {
            randomTarget = RandomTarget();
            agent.SetDestination(randomTarget);
            wandering = true;
        }

    }

    /// <summary>
    /// Checks current status of the enemy and exectues corresponding functions.
    /// </summary>
    void CheckState()
    {
        switch (state)
        {
            case EnemyState.idle:
                IdleUpdate();
                break;
            case EnemyState.chase:
                ChaseUpdate();
                break;
        }
    }

    /// <summary>
    /// Calculates the distance between the enemy and the player. If the distance is within the look radius, change status. Otherwise, keep wandering.
    /// </summary>
    void IdleUpdate()
    {
        if (distance <= lookRadius)
        {
            randomTarget = transform.position;
            state = EnemyState.chase;
            wandering = false;
            return;
        }
        else
        {
            Wander();
            if (Vector3.Distance(transform.position, randomTarget) <= 5.0f)
            {
                wandering = false;
                Debug.Log("arrived, changed random target");
            }

            if (rb.velocity.magnitude < 1.0f)
            {
                stuckTime++;
                if(stuckTime > 30)
                {
                    wandering = false;
                    stuckTime = 0;
                    Debug.Log("stuck, changed random target");
                }
            }
                
        }
    }

    /// <summary>
    /// Start chasing.
    /// </summary>
    void ChaseUpdate()
    {
        if (distance > lookRadius)
        {
            Debug.Log("lost the target");
            state = EnemyState.idle;
            return;
        }

        Debug.Log("chasing the target");
        agent.SetDestination(target.position);
    }
}
