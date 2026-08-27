using UnityEngine;

public class SkeletonMovement : BasicEnemyMovement
{
        [SerializeField]
        private float missRange;

        protected override void FollowPoint()
        { 
                Attack();
        }

        protected override void Attack()
        {
                if (attackTimer > 0) return;
                Instantiate(bite, followPosition + RandomPosAround(missRange), new Quaternion()).Init(Vector3.right, gameObject.GetHashCode());
                attackTimer = attackCD;
        }
}
