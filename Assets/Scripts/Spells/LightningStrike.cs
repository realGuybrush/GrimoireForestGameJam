using UnityEngine;

public class LightningStrike : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos)
    {
        Instantiate(projectile, cursorPos, new Quaternion());
    }
}
