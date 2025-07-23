public class NinjaIdleState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Animator.SetBool("IsRunning", false);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        if (PlayerController.MoveDirection != 0 && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaRunState));

        if (PlayerController.GetJumpKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaJumpState));
 
        if (PlayerController.GetAttackKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaAttackState));

        if (PlayerController.GetThrowKunaiKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaThrowKunaiState));
    }
}
