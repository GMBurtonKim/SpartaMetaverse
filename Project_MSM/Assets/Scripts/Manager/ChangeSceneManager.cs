using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    public static ChangeSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToFlappyScene()
    {
        SceneManager.LoadScene("FlappyAngelScene");
    }
}
