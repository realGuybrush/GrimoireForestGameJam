using UnityEngine;

public class LightningStrike : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos, int ignore)
    {
        Instantiate(projectile, cursorPos, new Quaternion()).Init(new Vector3(), ignore);
    }
}
