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
        if (PlayerController.IsBot)
        {
            if (CheckCanUpdate())
            {
                PlayerController.ThrowKunai();
                SetDelayTimer(PlayerController.KunaiFireRate);
            }
        }

        else
        {
            if (CheckCanUpdate())
            {
                PlayerController.EnableCanMove();
                PlayerController.EnableCanThrowKunai();

                if (PlayerController.MoveHorizontal == 0)
                    PlayerController.ChangeState(typeof(NinjaIdleState));

                if (PlayerController.MoveHorizontal != 0)
                    PlayerController.ChangeState(typeof(NinjaRunState));
            }
        }
    }
}
