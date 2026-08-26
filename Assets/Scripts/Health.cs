using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float hp;
    
    public event Action OnDie = delegate { };

    public void GetDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
            OnDie?.Invoke();
    }
}
