using System;
using System.Collections;
using UnityEngine;

public class NinjaThrowKunaiState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        if (PlayerController.CanThrowKunai)
        {
            PlayerController.ThrowKunai();
            SetDelayTimer(PlayerController.KunaiFireRate);
        }
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        if (CheckCanUpdate())
        {
            PlayerController.EnableCanMove();
            PlayerController.EnableCanThrowKunai();

            if (PlayerController.MoveDirection == 0)
                PlayerController.ChangeState(typeof(NinjaIdleState));

            if (PlayerController.MoveDirection != 0)
                PlayerController.ChangeState(typeof(NinjaRunState));
        }
    }
}
