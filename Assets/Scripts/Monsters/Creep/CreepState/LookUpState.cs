using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpState : CreepBaseState
{
    public override bool EnterState()
    {
        if (!FindPlayer()) return false;
        if ((player.transform.position.x > transform.position.x && isLeft) ||
            (player.transform.position.x < transform.position.x && !isLeft))
            return true;
        return false;
    }
    bool isLeft = true;
    
    public override void ExitState()
    {
        DoExitState.Invoke(UpdateState);
    }

    public override void UpdateState()
    {
        Flip();
    }
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.transform.localScale;
        theScale.x *= -1;
        transform.transform.localScale = theScale;
        isLeft = !isLeft;
    }

    public override void UpdateSkillBaseOnCharacterLv()
    {
    }
}
