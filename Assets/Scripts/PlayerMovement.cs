using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Animator animator, armsAnimator;

    [SerializeField]
    private Health health;

    [SerializeField]
    private TextMeshProUGUI hpText;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool lookingRight;

    [SerializeField]
    private Transform image, armsImage;

    [SerializeField]
    private Transform cursor;

    //todo: move all spells crap in manager
    [SerializeField]
    private UI_SpellManager spellManager;
    
    [SerializeField]
    private List<BasicSpell> spells;

    [SerializeField]
    private List<bool> acquiredSpells;

    private int activeSpellIndex;

    private List<float> spellTimers = new List<float>();

    private List<float> spellCDs = new List<float>();

    private List<ItemEnum> items = new List<ItemEnum>();

    [SerializeField]
    private SpeechBubble speechBubble;

    [SerializeField]
    private AudioSource oof;

    [SerializeField]
    private Exit exit, deathBed;

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
        health.OnDamaged += UpdateHealthText;
        SetSpellCDs();
    }
    
    private void Update()
    {
        if (cursor.transform.position.x > transform.position.x && !lookingRight ||
            cursor.transform.position.x < transform.position.x && lookingRight)
            Flip();
        UpdateSpellCDs();
    }

    private void HandleMove(InputAction.CallbackContext callbackContext)
    {
        rigidBody.linearVelocity = move.ReadValue<Vector2>() * speed;
        animator.SetBool("Walk", rigidBody.linearVelocity.magnitude > 0f);
        armsAnimator.SetBool("Walk", rigidBody.linearVelocity.magnitude > 0f);
    }

    private void HandleInteraction(InputAction.CallbackContext callbackContext)
    {
        if (speechBubble.IsTalking)
            speechBubble.Skip();
    }

    private void HandleAttack(InputAction.CallbackContext callbackContext)
    {
        if (speechBubble.IsTalking)
            speechBubble.Skip();
        else
            if (activeSpellIndex < spells.Count && acquiredSpells[activeSpellIndex] && spellTimers[activeSpellIndex] <= 0f)
            {
                spellManager.CastSpell(activeSpellIndex);
                armsAnimator.SetBool("Cast", true);
                spells[activeSpellIndex]?.Cast(cursor.position, transform, gameObject.GetHashCode());
                spellTimers[activeSpellIndex] = spellCDs[activeSpellIndex];
                StartCoroutine("StopCast");
            }
    }

    private IEnumerator StopCast()
    {
        yield return new WaitForSeconds(1f);
        armsAnimator.SetBool("Cast", false);
    }

    private void Handle1(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 0;
        spellManager.ChooseSpell(activeSpellIndex);
    }
    private void Handle2(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 1;
        spellManager.ChooseSpell(activeSpellIndex);
    }
    private void Handle3(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 2;
        spellManager.ChooseSpell(activeSpellIndex);
    }
    private void Handle4(InputAction.CallbackContext callbackContext)
    {
        activeSpellIndex = 3;
        spellManager.ChooseSpell(activeSpellIndex);
    }

    private void Flip()
    {
        image.eulerAngles = new Vector3(0f, image.eulerAngles.y > 1f?0f:180f, 0f);
        armsImage.eulerAngles = new Vector3(0f, armsImage.eulerAngles.y > 1f?0f:180f, 0f);
        lookingRight = !lookingRight;
    }

    private void SetSpellCDs()
    {
        foreach (var spell in spells)
        {
            spellCDs.Add(spell.CD);
            spellTimers.Add(0);
        }
    }

    private void UpdateSpellCDs()
    {
        for(int i = 0; i < spellTimers.Count; i++)
            if (spellTimers[i] > 0)
                spellTimers[i] -= Time.deltaTime; 
    }

    public void GetSpell(int index)
    {
        if(index < acquiredSpells.Count)
        {
            acquiredSpells[index] = true;
            spellManager.ActivateSpell(index, spells[index].CD);
            if(index == 0)
                spellManager.ChooseSpell(index);
        }
    }
    
    public void GetItem(ItemEnum newItem)
    {
        items.Add(newItem);
        CheckItems();
    }

    private void CheckItems()
    {
        if (items.Contains(ItemEnum.CatClaw) && items.Contains(ItemEnum.CrowBeak) &&
            items.Contains(ItemEnum.HunterMonocle) && items.Contains(ItemEnum.SkeletonSkull))
        {
            Instantiate(exit, Vector3.zero, new Quaternion());
            speechBubble.ShowMessage("What was that in the hut?");
        }
    }

    private void Die()
    {
        rigidBody.linearVelocity = Vector2.zero;
        animator.SetBool("Die", true);
        armsImage.gameObject.SetActive(false);
        Instantiate(deathBed, transform.position, new Quaternion());
        enabled = false;
    }

    private void UpdateHealthText(float value, bool damaged)
    {
        if(damaged)
            oof.Play();
        hpText.text = Mathf.Round(value).ToString();
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
        health.OnDamaged -= UpdateHealthText;
        move?.Disable();
        action?.Disable();
        attack?.Disable();
        key1?.Disable();
        key2?.Disable();
        key3?.Disable();
        key4?.Disable();
    }

    public bool Hidden => false;

    public List<ItemEnum> Items => items;
}
