using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public PlayerController selectedChar;
    public Image charImg;
    public PlayerController[] characters;
    public int charIndex;

    public Button charSelectButton, playButton, quitButton, arrowLeft, arrowRight, backButton;

    public GameObject characterSelectPanel, menuSelectPanel;

    void Start()
    {
        charSelectButton.onClick.AddListener(OnCharSelectButton);
        playButton.onClick.AddListener(OnPlayButton);
        quitButton.onClick.AddListener(OnQuitGameButton);
        backButton.onClick.AddListener(OnBackButton);

        arrowLeft.onClick.AddListener(() => SelectCharacter(-1));
        arrowRight.onClick.AddListener(() => SelectCharacter(1));
        SelectCharacter(0);
    }

    public void SelectCharacter(int direction)
    {
        if (direction > 0)
            charIndex++;
        else
            charIndex--;

        if (charIndex > characters.Length - 1)
        {
            charIndex = 0;
        }
        else if (charIndex < 0)
        {
            charIndex = characters.Length - 1;
        }

        selectedChar = characters[charIndex];
        charImg.sprite = selectedChar.charSprite;
    }

    public void OnCharSelectButton()
    {
        characterSelectPanel.SetActive(true);
        menuSelectPanel.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBackButton()
    {
        characterSelectPanel.SetActive(false);
        menuSelectPanel.SetActive(true);
    }

    public void OnQuitGameButton()
    {
        Application.Quit();
    }
}

public class CharacterSelector : MonoBehaviour
{

}
