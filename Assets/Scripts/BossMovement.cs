using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField]
    private QuestControls quests;
    
    [SerializeField]
    private Health health;

    [SerializeField]
    private BasicEnemyMovement controls;
    
    private int aggro = 1;

    private void Start()
    {
        health.SetPlotArmor(true);
        controls.BiterSetActive(false);
        controls.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>() != null) 
        {
            aggro--;
            if (aggro == 0)
            {
                quests.TellHimISaidF___YOU();
            }
        }
        if (aggro == -1)
        {
            //quests.ClearSpeech();
            quests.TurnOffTriggers();
            health.SetPlotArmor(false);
            controls.enabled = true;
            controls.BiterSetActive(true);
            enabled = false;
        }
    }
}
