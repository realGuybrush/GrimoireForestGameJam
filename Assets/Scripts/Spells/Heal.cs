using UnityEngine;

public class Heal : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos, int ignore)
    {
        Instantiate(projectile, playerPos).Init(new Vector3(), -1);
    }
}
