using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    private InputAction move, action, attack, key1, key2, key3, key4;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Health health;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool lookingRight;

    [SerializeField]
    private Transform image;

    [SerializeField]
    private Transform cursor;
    
    [SerializeField]
    private List<BasicSpell> spells;

    [SerializeField]
    private List<bool> acquiredSpells;

    private int activeSpellIndex;

    private void OnEnable()
    {
        move = playerInput.actions.FindAction("Move");
        action = playerInput.actions.FindAction("Interact");
        attack = playerInput.actions.FindAction("Attack");
        key1 = playerInput.actions.FindAction("1");
        key2 = playerInput.actions.FindAction("2");
        key3 = playerInput.actions.FindAction("3");
        key4 = playerInput.actions.FindAction("4");
        move?.Enable();
        action?.Enable();
        attack?.Enable();
        key1?.Enable();
        key2?.Enable();
        key3?.Enable();
        key4?.Enable();
        move.performed += HandleMove;
        move.canceled += HandleMove;
        action.started += HandleInteraction;
        attack.started += HandleAttack;
        key1.started += Handle1;
        key2.started += Handle2;
        key3.started += Handle3;
        key4.started += Handle4;
        health.OnDie += Die;
    }
    
    private void Update()
    {
        if (cursor.transform.position.x > transform.position.x && !lookingRight ||
            cursor.transform.position.x < transform.position.x && lookingRight)
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
        if(activeSpellIndex < spells.Count && acquiredSpells[activeSpellIndex])
            spells[activeSpellIndex]?.Cast(cursor.position, transform, gameObject.GetHashCode());
    }
    private void Handle1(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 0;
    }
    private void Handle2(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 1;
    }
    private void Handle3(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 2;
    }
    private void Handle4(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 3;
    }

    private void Flip()
    {
        image.eulerAngles = new Vector3(0f, image.eulerAngles.y > 1f?0f:180f, 0f);
        lookingRight = !lookingRight;
    }

    public void GetSpell(int index)
    {
        if(index < acquiredSpells.Count)
            acquiredSpells[index] = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        move.performed -= HandleMove;
        move.canceled -= HandleMove;
        action.started -= HandleInteraction;
        attack.started -= HandleAttack;
        key1.started -= Handle1;
        key2.started -= Handle2;
        key3.started -= Handle3;
        key4.started -= Handle4;
        health.OnDie -= Die;
        move?.Disable();
        action?.Disable();
        attack?.Disable();
        key1?.Disable();
        key2?.Disable();
        key3?.Disable();
        key4?.Disable();
    }

    public bool Hidden => false;
}
