using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyGameManager : MiniGameManagerBase
{
    [SerializeField] private FlappyUIManager uiManager;

    private float scoreDelayTime = 0.5f;
    private float lastRestartTime = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBestScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager = FindObjectOfType<FlappyUIManager>();
    }

    public override void StartGame()
    {
        base.StartGame();
        lastRestartTime = Time.time;
        uiManager?.UpdateScoreUI(currentScore);
    }

    public override void AddScore(int score)
    {
        if (Time.time - lastRestartTime < scoreDelayTime) return;

        base.AddScore(score);
        uiManager?.UpdateScoreUI(currentScore);
    }

    public override void GameOver()
    {
        base.GameOver();
        SaveBestScore();
        uiManager?.UpdateScoreUI(currentScore, bestScore);
        uiManager?.ShowEndUI();
    }

    public override void Restart()
    {
        base.Restart();
        StartGame();
        ResetPlayer();
        ResetObstacles();
        uiManager?.ShowGameUI();
    }

    private void SaveBestScore()
    {
        int savedBest = PlayerPrefs.GetInt("BestScore", 0);

        if (bestScore > savedBest)
        {
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.SetInt("IsNewBestScore", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsNewBestScore", 0);
        }

        PlayerPrefs.Save();
    }

    private void LoadBestScore() =>
        bestScore = PlayerPrefs.GetInt("BestScore_Flappy", 0);

    private void ResetPlayer()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null && player.TryGetComponent(out FlappyPlayer flappy))
        {
            player.transform.position = Vector3.zero;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            flappy.ResetPlayer();
        }
    }

    private void ResetObstacles()
    {
        var looper = FindObjectOfType<Looper>();
        if (looper != null)
            looper.ResetObstacles();
    }
}