using System;
using System.Collections.Generic;

[Serializable]
public class CharacterState
{
    public CharacterStateBase currentState;
    public Dictionary<Type, CharacterStateBase> statesDict = new();
    public void ChangeState(CharacterStateBase newState)
    {
        if (currentState)
            currentState.OnStateExit();

        currentState = newState;
        currentState.OnStateEnter();
    }

    public void RegisterCharacterState(List<CharacterStateBase> C_states)
    {
        foreach (var item in C_states)
        {
            Type type = item.GetType();
            statesDict[type] = item;
        }

    }
}