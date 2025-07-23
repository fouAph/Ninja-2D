using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform spawnPostitionTransform;
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] GameObject GameOverPanel;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        var p = CharacterSelectHelper.Instance.SpawnSelectedCharacter(spawnPostitionTransform);
        cinemachineCamera.Target.TrackingTarget = p.transform;
    }

    void OnDestroy()
    {
        if(CharacterSelectHelper.Instance)
        DestroyImmediate(CharacterSelectHelper.Instance.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ShowGameOverPanel();
    }

    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
