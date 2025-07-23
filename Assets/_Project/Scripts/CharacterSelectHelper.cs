using Unity.Mathematics;
using UnityEngine;

public class CharacterSelectHelper : MonoBehaviour
{
    public static CharacterSelectHelper Instance;
    public PlayerController selectedChar;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetSelectedCharacter(PlayerController _selectedChar)
    {
        selectedChar = _selectedChar;
    }

    public PlayerController SpawnSelectedCharacter(Transform spawnPoint)
    {
        return Instantiate(selectedChar, spawnPoint.position, quaternion.identity); 
    }
}
