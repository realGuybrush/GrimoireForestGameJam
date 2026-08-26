using System;
using UnityEngine;

public class PlayerSeekerTrigger : MonoBehaviour
{
    public event Action<PlayerMovement> OnPlayerEnter = delegate { };
    public event Action OnPlayerStay = delegate { };
    public event Action OnPlayerLeave = delegate { };
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        OnPlayerEnter?.Invoke(player);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        OnPlayerStay?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null) return;
        OnPlayerLeave?.Invoke();
    }
}
