using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifeTime, speed;

    [SerializeField]
    private Rigidbody2D body;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        body.linearVelocity = transform.right * speed;
    }
}
