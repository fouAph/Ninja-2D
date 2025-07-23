public class NinjaRunState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Animator.SetBool("IsRunning", true);

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        PlayerController.FlipCharacter();
        PlayerController.Move();

        if (PlayerController.MoveDirection == 0)
            PlayerController.ChangeState(typeof(NinjaIdleState));

        if (PlayerController.GetJumpKey())
            PlayerController.ChangeState(typeof(NinjaJumpState));

        if (PlayerController.GetThrowKunaiKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaThrowKunaiState));

        if (PlayerController.GetAttackKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaAttackState));
    }
}
