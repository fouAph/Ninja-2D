using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    public GameObject GameOverPanel;

    void OnTriggerEnter2D(Collider2D collision)
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

