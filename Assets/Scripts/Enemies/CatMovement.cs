using UnityEngine;

public class CatMovement : BasicEnemyMovement
{
    [SerializeField]
    private float dashCD;

    private float dashTimer;

    private Vector3 intermittentFollowPosition, attackDir;

    private Projectile projectile;

    private void Start()
    {
        intermittentFollowPosition = transform.position;
    }

    protected override void FollowPoint()
    {
        if((intermittentFollowPosition - transform.position).magnitude < 0.3f)
        {
            body.linearVelocity = Vector2.zero;
        }
        if(dashTimer <= 0)
        {
            intermittentFollowPosition = 2*followPosition - transform.position;
            Debug.Log(intermittentFollowPosition);
            attackDir = (intermittentFollowPosition - transform.position).normalized;
            if(projectile != null) Destroy(projectile);
            body.linearVelocity = attackDir * speed;
            Attack();
            dashTimer = dashCD;
        } else
        {
            dashTimer -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        if (projectile != null) return;
        projectile = Instantiate(bite, transform);
        projectile.Init(attackDir, gameObject.GetHashCode());
    }
}
