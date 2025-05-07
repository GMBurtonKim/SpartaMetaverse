using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSceneScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private GameObject newBestImage;
    [SerializeField] private Button closeImageButton;

    private void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"{bestScore}";

        bool isNewBest = PlayerPrefs.GetInt("IsNewBestScore", 0) == 1;

        if (newBestImage != null)
        {
            newBestImage.SetActive(isNewBest);
        }

        PlayerPrefs.SetInt("IsNewBestScore", 0);

        closeImageButton.onClick.AddListener(OnClickCloseImage);
    }

    private void OnClickCloseImage()
    {
        if (newBestImage != null)
        {
            newBestImage.SetActive(false);
            return;
        }
    }
}
