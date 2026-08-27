using UnityEngine;

public class BasicSpell : Item
{
    [SerializeField]
    protected Projectile projectile;

    [SerializeField]
    private int index;

    [SerializeField]
    private float cd;
    
    public virtual void Cast(Vector3 cursorPos, Transform playerPos, int ignore)
    {
        
    }

    protected override void TriggerAction(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        player.GetSpell(index);
        Destroy(gameObject);
    }

    public float CD => cd;
}
