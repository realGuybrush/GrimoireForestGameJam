using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemEnum itemIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerAction(other);
    }

    protected virtual void TriggerAction(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        player.GetItem(itemIndex);
        Destroy(gameObject);
    }
}
