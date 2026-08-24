using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    private InputAction move, action, attack;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool lookingRight;

    private bool defaultLookingRight;

    [SerializeField]
    private Transform image;

    private void OnEnable()
    {
        move = playerInput.actions.FindAction("Move");
        action = playerInput.actions.FindAction("Interact");
        attack = playerInput.actions.FindAction("Attack");
        move?.Enable();
        action?.Enable();
        attack?.Enable();
        move.performed += HandleMove;
        move.canceled += HandleMove;
        action.started += HandleInteraction;
        attack.started += HandleAttack;
        defaultLookingRight = lookingRight;
    }
    
    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        if (pos.x > transform.position.x && !lookingRight ||
            pos.x < transform.position.x && lookingRight)
            Flip();
    }

    private void HandleMove(InputAction.CallbackContext callbackContext)
    {
        rigidBody.linearVelocity = move.ReadValue<Vector2>() * speed;
    }

    private void HandleInteraction(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("E");
    }

    private void HandleAttack(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Attack");
    }

    private void Flip()
    {
        image.eulerAngles = new Vector3(0f, image.eulerAngles.y > 1f?0f:180f, 0f);
        lookingRight = !lookingRight;
    }

    private void OnDisable()
    {
        move.performed -= HandleMove;
        move.canceled -= HandleMove;
        action.started -= HandleInteraction;
        attack.started -= HandleAttack;
        move?.Enable();
        action?.Enable();
        attack?.Enable();
    }
}
