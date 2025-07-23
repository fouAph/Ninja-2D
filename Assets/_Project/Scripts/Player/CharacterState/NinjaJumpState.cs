using System.Collections;
using UnityEngine;

public class NinjaJumpState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Jump();
        SetDelayTimer(.05f);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        PlayerController.FlipCharacter();
        PlayerController.Move();
        if (CheckCanUpdate())
        {
            if (PlayerController.IsGrounded)
                PlayerController.ChangeState(typeof(NinjaIdleState));

            if (PlayerController.GetThrowKunaiKey() && PlayerController.IsBot == false)
                PlayerController.ChangeState(typeof(NinjaThrowKunaiState));

            if (PlayerController.GetAttackKey() && PlayerController.IsBot == false)
                PlayerController.ChangeState(typeof(NinjaAttackState));
        }
    }
}
