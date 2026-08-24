using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    [SerializeField]
    protected Projectile projectile;

    [SerializeField]
    private int index;
    
    public virtual void Cast(Vector3 cursorPos, Transform playerPos)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerMovement>()?.GetSpell(index);
        Destroy(gameObject);
    }
}
