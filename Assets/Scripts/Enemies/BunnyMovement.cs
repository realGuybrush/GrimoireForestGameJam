using UnityEngine;

public class BunnyMovement : BasicEnemyMovement
{
    private Vector3 intermittentFollowPosition;

    [SerializeField]
    private float jumpDistance, chanceToRandomJump, jumpLag;

    private float jumpCD = 0;
    
    private void Start()
    {
        intermittentFollowPosition = transform.position;
    }
    
    protected override void FollowPoint()
    {
        if (jumpCD <= 0)
        {
            if ((intermittentFollowPosition - transform.position).magnitude > 0.1f) 
                body.linearVelocity = (intermittentFollowPosition - transform.position).normalized * speed;
            else
            {
                body.linearVelocity = Vector2.zero;
                if (Random.Range(0, 100) < chanceToRandomJump)
                    intermittentFollowPosition = RandomPosAround(jumpDistance);
                else
                    intermittentFollowPosition = (followPosition - transform.position).normalized * jumpDistance;
                jumpCD = jumpLag;
            }
        } else
        {
            jumpCD -= Time.deltaTime;
        }
    }
}
