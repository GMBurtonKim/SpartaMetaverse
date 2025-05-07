using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlappyUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject endUI;

    [Header("Btn")]
    public Button startBtn;
    public Button retryBtn;
    public Button goMainBtn;

    [Header("Txt")]
    public TextMeshProUGUI scoreTxtGame;
    public TextMeshProUGUI scoreTxtEnd;
    public TextMeshProUGUI bestScoreTxt;

    private void Start()
    {
        startBtn.onClick.AddListener(OnClickStartButton);
        retryBtn.onClick.AddListener(OnClickRetryButton);
        goMainBtn.onClick.AddListener(OnClickGoMainButton);


        startUI.SetActive(false);
        gameUI.SetActive(false);
        endUI.SetActive(false);
        ShowStartUI();
    }

    public void ShowStartUI()
    {
        startUI.SetActive(true);
        UpdateScoreUI(0);
    }

    public void ShowGameUI()
    {
        startUI.SetActive(false);
        gameUI.SetActive(true);
        endUI.SetActive(false);
    }

    public void ShowEndUI()
    {
        endUI.SetActive(true);
    }

    public void OnClickStartButton()
    {
        ShowGameUI();
        FlappyGameManager.Instance.StartGame();
    }

    public void OnClickRetryButton()
    {
        FlappyGameManager.Instance.Restart();
    }

    public void OnClickGoMainButton()
    {
        ChangeSceneManager.Instance.ToMainScene();
    }

    public void UpdateScoreUI(int score, int bestScore = -1)
    {
            scoreTxtGame.text = score.ToString();
            scoreTxtEnd.text = score.ToString();

        if (bestScore >= 0)
            bestScoreTxt.text = bestScore.ToString();
    }
}
