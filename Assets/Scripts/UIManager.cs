using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public ProgressBar pb;
    public float value;

    #region Coins public to give referance to other scripts
    [Header("Coins")]
    public TextMeshProUGUI coinText;
    public GameObject coinPanel;
    public GameObject coinPanel2;
    public GameObject coinPanel3;
    #endregion

    [SerializeField]
    private TextMeshProUGUI woodText;

    #region PausePanel
    [Header("Pause Panel")]
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button menuButton;
    #endregion

    //public TextMeshProUGUI progressText;
    [HideInInspector] public int score = 0;
    //private int progressScore = 0;
    private bool isPaused;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }


    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(LoadMenuScene);
        instance = this;
        RefreshUI();
    }


    //pause game
    private void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);
        }
    }

    //resume game
    private void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
        }
    }

    //progerss bar
    public void ProgressBar()
    {
        pb.BarValue = value;
    }

    //increase score
    public void IncreaseScore(int increament)
    {
        score += increament;
        RefreshUI();
    }

    public void DecreaseScore(int decreament)
    {
        score -= decreament;
        RefreshUI();
    }

    //refresh text
    private void RefreshUI()
    {
        woodText.text =  ""+ score;
    }

    //update progress bar
    //public void UpdateProgerssBarText(int score)
    //{
    //    progressText.text = "/" + progressScore;
    //}

    //restart game
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    //load menu/lobby scene
    private void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
        pausePanel.SetActive(false);
    }

    public IEnumerator PanelOnOff()
    {
        coinPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        coinPanel.SetActive(false);
    }
}
