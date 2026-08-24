using UnityEngine;

public class Fireball : BasicSpell
{
    public override void Cast(Vector3 cursorPos, Transform playerPos)
    {
        Instantiate(projectile, playerPos.position, new Quaternion()).transform.right = cursorPos - playerPos.position;
    }
}
