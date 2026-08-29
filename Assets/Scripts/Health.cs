using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float hp;

    private float baseHP;

    private bool invulnerable;
    
    public event Action<float> OnDamaged = delegate { };
    
    public event Action OnDie = delegate { };

    private void Awake()
    {
        baseHP = hp;
    }

    public void GetDamage(float damage)
    {
        if (invulnerable) return;
        hp -= damage;
        if(hp <= 0)
            OnDie?.Invoke();
        if (hp > baseHP) hp = baseHP;
        OnDamaged?.Invoke(hp);
    }

    public void SetPlotArmor(bool value)
    {
        invulnerable = value;
    }
}
