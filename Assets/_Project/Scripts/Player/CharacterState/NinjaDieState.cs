using Unity.VisualScripting;

public class NinjaDieState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Animator.SetTrigger("Die");
        SetDelayTimer(1);
        PlayerController.FreezeCharacter();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
        if (CheckCanUpdate())
        {

        }
    }
}
