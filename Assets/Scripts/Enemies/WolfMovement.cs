using UnityEngine;

public class WolfMovement : BasicEnemyMovement
{
    protected override void FollowPoint()
    {
        if((followPosition - transform.position).magnitude > 1f)
            body.linearVelocity = (followPosition - transform.position).normalized * speed;
        else
            body.linearVelocity = followPosition - transform.position;
    }
}
