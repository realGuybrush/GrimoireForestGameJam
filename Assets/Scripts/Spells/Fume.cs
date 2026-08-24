using UnityEngine;

public class Fume : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos)
    {
        Instantiate(projectile, playerPos);
    }
}
