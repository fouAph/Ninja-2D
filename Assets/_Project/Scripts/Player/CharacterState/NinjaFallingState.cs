public class NinjaFallingState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Animator.SetBool("IsFalling", true);
    }

    public override void OnStateExit()
    {
        PlayerController.Animator.SetBool("IsFalling", false);
    }

    public override void OnStateRunning()
    {
        PlayerController.FlipCharacter();
        PlayerController.Move();


        if (PlayerController.IsGrounded)
            PlayerController.ChangeState(typeof(NinjaIdleState));
            
        if (PlayerController.GetGlidingKey() && PlayerController.GetVelocityY() < 0)
        {
            PlayerController.SetGravityScale(PlayerController.glidingGravity);
            PlayerController.Gliding();
        }
        else
        {
            PlayerController.SetGravityScale(1);
            PlayerController.Animator.SetBool("IsGliding", false);
        }


        if (PlayerController.GetThrowKunaiKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaThrowKunaiState));

        if (PlayerController.GetAttackKey() && PlayerController.IsBot == false)
            PlayerController.ChangeState(typeof(NinjaAttackState));

    }
}