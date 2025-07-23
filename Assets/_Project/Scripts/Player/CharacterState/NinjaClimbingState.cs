public class NinjaClimbingState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.StartClimbing();
        SetDelayTimer(.2f);
    }

    public override void OnStateExit()
    {
        PlayerController.StopClimbing();
    }

    public override void OnStateRunning()
    {
        PlayerController.Climbing();
        if (CheckCanUpdate())
        {
            if (PlayerController.MoveHorizontal == 0 && PlayerController.IsGrounded || PlayerController.CurrentLadder == null)
                PlayerController.ChangeState(typeof(NinjaIdleState));

            else if (PlayerController.CurrentLadder == null && !PlayerController.IsGrounded && PlayerController.IsBot == false)
                PlayerController.ChangeState(typeof(NinjaJumpState));
        }
    }
}