using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int playerLife = 3;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score=0;
    void Awake()
    {
        int numGameSessions=FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        healthText.text = playerLife.ToString();
        scoreText.text = score.ToString();
    }
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLife > 1) TakeLife();
        else ResetSession();
    }

    private void TakeLife()
    {
        playerLife--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        healthText.text = playerLife.ToString();
    }

    private void ResetSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
