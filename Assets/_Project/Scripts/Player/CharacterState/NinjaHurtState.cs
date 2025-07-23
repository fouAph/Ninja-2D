public class NinjaHurtState : CharacterStateBase
{
    public override void OnStateEnter()
    {
        PlayerController.Hurt();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRunning()
    {
    }
}
