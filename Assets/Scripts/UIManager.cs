using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public ProgressBar pb;
    public float value;
    public TextMeshProUGUI coinText;
    public GameObject coinPanel;
    public GameObject coinPanel2;
    public GameObject coinPanel3;
    [SerializeField] private TextMeshProUGUI woodText;
    public TextMeshProUGUI progressText;
    private int score = 0;
    private int progressScore = 0;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }

        RefreshUI();
    }


    public void ProgressBar()
    {
        pb.BarValue = value;
    }

    public void IncreaseScore(int increament)
    {
        score += increament;
        RefreshUI();
    }

    private void RefreshUI()
    {
        woodText.text =  ""+ score;
    }

    public void UpdateProgerssBarText(int score)
    {
        progressText.text = "/" + progressScore;
    }
}
