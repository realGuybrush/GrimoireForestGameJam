using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HunterMovement : BasicEnemyMovement
{
    [SerializeField]
    private float chaseDistanceMax, chaseDistanceMin, dashCD;

    private float dashTimer;

    private Vector3 intermittentFollowPosition;

    private void Start()
    {
        intermittentFollowPosition = transform.position;
    }

    protected override void FollowPoint()
    {
        if(dashTimer <= 0)
        {
            if((intermittentFollowPosition - transform.position).magnitude < 0.3f)
            {
                intermittentFollowPosition = new Vector3(followPosition.x + Random.Range(chaseDistanceMin, chaseDistanceMax),
                followPosition.y + Random.Range(chaseDistanceMin, chaseDistanceMax));
                dashTimer = dashCD;
            } else
            {
                body.linearVelocity = intermittentFollowPosition - transform.position;
                Attack();
            }
        } else
        {
            dashTimer -= Time.deltaTime;
        }
    }
}
