using System.Collections.Generic;
using UnityEngine;

public class EnemyPathWithBranch : MonoBehaviour
{
    public List<Transform> waypointsList1; 
    public List<Transform> waypointsList2; 

    public Transform branchPoint; 

    public float speed = 2f;
    public float reachThreshold = 0.2f;
    public float branchChanceIncrement = 0.1f; 

    private List<Transform> currentWaypoints; 
    private int currentWaypointIndex;
    private bool movingForward = true;
    private float currentBranchChance = 0f; 

    private void Start()
    {
        
        currentWaypoints = waypointsList1;
        currentWaypointIndex = 0;
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (currentWaypoints.Count == 0) return;

        Transform targetWaypoint = currentWaypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            
            if (targetWaypoint == branchPoint)
            {
                TrySwitchBranch();
            }

            UpdateWaypointIndex();
        }

        
        transform.LookAt(targetWaypoint);
    }

    private void TrySwitchBranch()
    {
        
        if (Random.value < currentBranchChance)
        {
            
            if (currentWaypoints == waypointsList1)
            {
                currentWaypoints = waypointsList2;
                currentWaypointIndex = GetNextWaypointIndexFromBranch(waypointsList2);
            }
            else
            {
                currentWaypoints = waypointsList1;
                currentWaypointIndex = GetNextWaypointIndexFromBranch(waypointsList1);
            }

            
            currentBranchChance = 0f;
        }
        else
        {
            
            currentBranchChance += branchChanceIncrement;
        }
    }

    private int GetNextWaypointIndexFromBranch(List<Transform> waypoints)
    {
        
        if (Random.value > 0.5f)
        {
            
            return waypoints.IndexOf(branchPoint) + 1;
        }
        else
        {
            
            return waypoints.IndexOf(branchPoint) - 1;
        }
    }

    private void UpdateWaypointIndex()
    {
        if (movingForward)
        {
            if (currentWaypointIndex < currentWaypoints.Count - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                movingForward = false;
                currentWaypointIndex--;
            }
        }
        else
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                movingForward = true;
                currentWaypointIndex++;
            }
        }
    }
}
