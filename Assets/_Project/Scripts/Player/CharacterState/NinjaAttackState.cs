using System.Collections;
using UnityEngine;

public class NinjaAttackState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        if (PlayerController.CanAttack)
        {
            PlayerController.MeleeAttack();
            SetDelayTimer(PlayerController.attackCoolDown);
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
            PlayerController.EnableCanAttack();

            if (PlayerController.MoveHorizontal == 0)
                PlayerController.ChangeState(typeof(NinjaIdleState));

            if (PlayerController.MoveHorizontal != 0)
                PlayerController.ChangeState(typeof(NinjaRunState));
        }
    }
}
