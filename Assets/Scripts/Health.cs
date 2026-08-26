using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float hp;

    private bool invulnerable;
    
    public event Action OnDie = delegate { };

    public void GetDamage(float damage)
    {
        if (invulnerable) return;
        hp -= damage;
        if(hp <= 0)
            OnDie?.Invoke();
    }

    public void SetPlotArmor(bool value)
    {
        invulnerable = value;
    }
}
