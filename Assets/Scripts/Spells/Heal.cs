using UnityEngine;

public class Heal : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos)
    {
        Instantiate(projectile, playerPos);
    }
}
