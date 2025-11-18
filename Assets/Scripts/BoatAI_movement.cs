using UnityEngine;

public class BoatAI : MonoBehaviour
{
    
    public Transform[] waypoints;

    public float [] stopTimes;

    public float moveSpeed = 2f;

    public Transform sprite;   // assign your sprite child object here

    private int currentWaypoint = 0;

    private bool isStopped = false;

    private float stopTimer = 0f;

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (isStopped)
        {
            HandleStopTimer();
        }

        else
        {
            
        MoveToWaypoint();
        FaceCorrectDirection();
        }
    }

    void MoveToWaypoint()
    {
        Vector2 target = waypoints[currentWaypoint].position;

        // Move straight toward the waypoint
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // When arrived, switch to next waypoint
        if (Vector2.Distance(transform.position, target) < 0.05f)
        {

            StartStopAtWaypoint();


            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }


     void StartStopAtWaypoint()
    {
        // Is there a stop time defined for this waypoint?
        float stopTime = (currentWaypoint < stopTimes.Length) ? stopTimes[currentWaypoint] : 0f;

        if (stopTime > 0f)
        {
            isStopped = true;
            stopTimer = stopTime;
        }
    }

    void HandleStopTimer()
    {
        stopTimer -= Time.deltaTime;

        if (stopTimer <= 0f)
        {
            isStopped = false;
        }
    }


    void FaceCorrectDirection()
    {
        Vector2 direction = waypoints[currentWaypoint].position - transform.position;

        // If moving right -> face right
        if (direction.x > 0.01f)
        {
            sprite.localScale = new Vector3(1, 1, 1);
        }
        // If moving left -> face left
        else if (direction.x < -0.01f)
        {
            sprite.localScale = new Vector3(-1, 1, 1);
        }
    }

}
