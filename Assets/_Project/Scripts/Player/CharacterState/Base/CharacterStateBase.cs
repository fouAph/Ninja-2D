using System.Threading;
using UnityEngine;

public abstract class CharacterStateBase : MonoBehaviour
{
    protected PlayerController PlayerController => GetComponent<PlayerController>();
    protected float currentTimer;
    public abstract void OnStateEnter();

    public abstract void OnStateRunning();

    public abstract void OnStateExit();



    protected void SetDelayTimer(float timer)
    {
        currentTimer = timer;
    }

    protected bool CheckCanUpdate()
    {
        currentTimer -= Time.deltaTime;
        return currentTimer <= 0;
    }
}
