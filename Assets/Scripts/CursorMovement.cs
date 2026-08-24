using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Mouse.current.position.value);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
