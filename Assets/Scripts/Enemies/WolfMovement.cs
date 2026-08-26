using UnityEngine;

public class WolfMovement : BasicEnemyMovement
{
    protected override void FollowPoint()
    {
        body.linearVelocity = (followPosition - transform.position).normalized * speed;
    }
}
