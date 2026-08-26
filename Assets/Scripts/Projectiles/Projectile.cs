using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float damage;
    
    [SerializeField]
    private bool destroyOnHit;
    
    [SerializeField]
    private int ignore;
    
    [SerializeField]
    private float lifeTime, speed;

    [SerializeField]
    private Rigidbody2D body;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        body.linearVelocity = transform.right * speed;
    }

    public void Init(Vector3 direction, int newIgnore)
    {
        ignore = newIgnore;
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetHashCode() == ignore) return;
        other.GetComponent<Health>().GetDamage(damage);
        if(destroyOnHit) Destroy(gameObject);
    }
}
