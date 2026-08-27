using UnityEngine;

public class HealProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetHashCode() != ignore) return;
        other.GetComponent<Health>().GetDamage(damage);
    }
}
