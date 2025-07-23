public class NinjaIdleState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Animator.SetBool("IsRunning", false);
        PlayerController.SetGravityScale(1);
        PlayerController.Gliding(true);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        if (PlayerController.MoveHorizontal != 0 && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaRunState));

        if (PlayerController.GetClimbingKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaClimbingState));

        if (PlayerController.GetJumpKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaJumpState));

        if (PlayerController.GetAttackKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaAttackState));

        if (PlayerController.GetThrowKunaiKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaThrowKunaiState));

        if (PlayerController.GetVelocityY() < 0)
        {
            PlayerController.ChangeState(typeof(NinjaFallingState));
        }
    }
}
