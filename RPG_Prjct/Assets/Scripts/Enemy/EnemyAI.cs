using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float chaseSpeed = 5f; 
    public float normalSpeed = 2f; 
    private Transform player; 
    private bool isChasing = false; 
    public float viewRadius = 10f; 
    public float obstacleCheckDistance = 1f; 

    [SerializeField] private float stopDistance = 2f; 

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
            MoveTowardsPlayer(); 
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null)
        {
            isChasing = false; 
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position); 

        
        if (distance > stopDistance)
        {
            
            RaycastHit hit;
            Vector3 direction = (player.position - transform.position).normalized; 

            if (!Physics.Raycast(transform.position, direction, out hit, obstacleCheckDistance))
            {
                
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                transform.LookAt(player); 
            }
        }
        else
        {
            
            transform.LookAt(player); 
        }
    }
}
