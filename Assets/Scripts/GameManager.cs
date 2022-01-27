using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    
    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    void OnPlayButtonClick()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    void OnQuitButtonClick()
    {
        Application.Quit();
    }
  
}