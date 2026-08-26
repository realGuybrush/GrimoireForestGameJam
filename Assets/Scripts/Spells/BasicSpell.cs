using System;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    [SerializeField]
    protected Projectile projectile;

    [SerializeField]
    private int index;
    
    public virtual void Cast(Vector3 cursorPos, Transform playerPos, int ignore)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        player.GetSpell(index);
        Destroy(gameObject);
    }
}
