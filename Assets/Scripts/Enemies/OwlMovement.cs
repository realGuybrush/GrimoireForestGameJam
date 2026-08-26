using System.Collections.Generic;
using UnityEngine;

public class OwlMovement : BasicEnemyMovement
{
    private Dictionary<int, Vector3> sinCos = 
        new Dictionary<int, Vector3>() { {0, new Vector3 (1f, 0f)}, 
                                         {15, new Vector3 (0.9659f, 0.2588f)}, 
                                         {30, new Vector3 (0.866f, 0.5f)}, 
                                         {45, new Vector3 (0.7071f, 0.7071f)}, 
                                         {60, new Vector3 (0.5f, 0.866f)}, 
                                         {75, new Vector3 (0.2588f, 0.9659f)}, 
                                         {90, new Vector3 (0f, 1f)}, 
                                         {105, new Vector3 (-0.2588f, 0.9659f)}, 
                                         {120, new Vector3 (-0.5f, 0.866f)}, 
                                         {135, new Vector3 (-0.7071f, 0.7071f)}, 
                                         {150, new Vector3 (-0.866f, 0.5f)}, 
                                         {165, new Vector3 (-0.9659f, 0.2588f)}, 
                                         {180, new Vector3 (-1f, 0f)}, 
                                         {195, new Vector3 (-0.9659f, -0.2588f)}, 
                                         {210, new Vector3 (-0.866f, -0.5f)}, 
                                         {225, new Vector3 (-0.7071f, -0.7071f)}, 
                                         {240, new Vector3 (-0.5f, -0.866f)}, 
                                         {255, new Vector3 (-0.2588f, -0.9659f)}, 
                                         {270, new Vector3 (0f, -1f)}, 
                                         {285, new Vector3 (0.2588f, -0.9659f)}, 
                                         {300, new Vector3 (0.5f, -0.866f)}, 
                                         {315, new Vector3 (0.7071f, -0.7071f)}, 
                                         {330, new Vector3 (0.866f, -0.5f)}, 
                                         {345, new Vector3 (0.9659f, -0.2588f)}};

    private Vector3 intermittentFollowPosition;
    
    [SerializeField]
    private float circlingDistance;
    
    private void Start()
    {
        intermittentFollowPosition = transform.position;
    }
    
    protected override void FollowPoint()
    {
        if (followingPlayer)
        {
            if ((transform.position - followPosition).magnitude < circlingDistance)
            {
                if ((transform.position - intermittentFollowPosition).magnitude < 0.3f)
                {
                    var targetToOwlBeam = transform.position - followPosition;
                    var angle = CalculateNextAngle(targetToOwlBeam);
                    intermittentFollowPosition = followPosition + sinCos[angle] * targetToOwlBeam.magnitude ;
                } else
                {
                    body.linearVelocity = (intermittentFollowPosition - transform.position).normalized * speed;
                    Attack();
                }
            } else
            {
                body.linearVelocity = (followPosition - transform.position).normalized * speed;
                intermittentFollowPosition = transform.position;
            }
        }
        else if(body.linearVelocity.magnitude > 0) body.linearVelocity = Vector2.zero;
    }

    private int CalculateNextAngle(Vector3 targetToOwlBeam)
    {
        var vector = targetToOwlBeam.normalized;
        var angle = Mathf.Rad2Deg * Mathf.Acos(vector.x);
        if (vector.y < 0) angle = -angle + 360;
        angle = Mathf.Round(angle/15)*15 + 15;
        if (angle >= 360) angle -= 360;
        return (int)angle;
    }
}
