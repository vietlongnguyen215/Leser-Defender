using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour // duong di cho Enemy
{
    WareConfig wareConfig;
    List<Transform> waypoints;
    [SerializeField] bool looping = false;
    //[SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = wareConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WareConfig wareConfig)
    {
        this.wareConfig = wareConfig;
    }

    private void Move()
    {
        
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = wareConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards
                    (transform.position, targetPosition, movementThisFrame);
                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        
        
    }
}
