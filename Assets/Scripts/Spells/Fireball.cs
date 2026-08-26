using UnityEngine;

public class Fireball : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos, int ignore)
    {
        Instantiate(projectile, playerPos.position, new Quaternion()).Init(cursorPos - playerPos.position, ignore);
    }
}
