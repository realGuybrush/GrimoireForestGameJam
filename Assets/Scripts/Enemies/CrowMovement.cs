using UnityEngine;

public class CrowMovement : OwlMovement
{
        protected override void Attack()
        {
                if (attackTimer > 0) return;
                Instantiate(bite, followPosition, new Quaternion()).Init(Vector3.right, gameObject.GetHashCode());
                attackTimer = attackCD;
        }
}
