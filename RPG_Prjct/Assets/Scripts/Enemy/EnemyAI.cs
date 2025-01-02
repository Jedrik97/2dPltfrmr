using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float normalSpeed = 2f;
    private Transform player;
    public bool isChasing = false;
    public float viewRadius;
    public float obstacleCheckDistance = 1f;

    private FieldOfView fieldOfView;
    private void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (fieldOfView._targets.Count > 0)
        {
            player = fieldOfView._targets[0];
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing && player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (player == null)
        {
            isChasing = false; 
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance > viewRadius) 
        {
            isChasing = false; 
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, obstacleCheckDistance))
            {
                if (hit.collider != null)
                {
                    return;
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            transform.LookAt(player);
        }
    }
}
