using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerSeekerTrigger talk, take;

    [SerializeField]
    private Health health;

    [SerializeField]
    private BasicEnemyMovement controls;

    private int aggro = 1;

    private void Start()
    {
        talk.OnPlayerEnter += Talk;
        take.OnPlayerEnter += TakeItem;
        health.SetPlotArmor(true);
        controls.BiterSetActive(false);
        controls.enabled = false;
    }

    private void Talk(PlayerMovement player)
    {
        Debug.Log("Hello.");
    }
    
    private void TakeItem(PlayerMovement player)
    {
        Debug.Log("Thank you.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>() != null) aggro--;
        if (aggro == 0)
        {
            //say FU
        }
        if (aggro == -1)
        {
            health.SetPlotArmor(false);
            controls.enabled = true;
            controls.BiterSetActive(true);
            enabled = false;
        }
    }

    private void OnDestroy()
    {
        talk.OnPlayerEnter -= Talk;
        take.OnPlayerEnter -= TakeItem;
    }
}
