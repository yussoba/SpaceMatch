using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int _scoreToAdd;
    private Bunker[] _bunkers;

    private void Start()
    {
        _bunkers = FindObjectsOfType<Bunker>();
    }
    public void CountScore(int length)
    {
        FibonacciAddScore(10, 10, 1, length);
        scoreText.text = (Int32.Parse(scoreText.text) + _scoreToAdd).ToString();
        _scoreToAdd = 0;
    }

    private void FibonacciAddScore(int value1, int value2, int counter, int length)
    {
        _scoreToAdd = (counter - 1) * value1;
        if (counter <= length)
        {
            FibonacciAddScore(value2, value1 + value2, counter + 1, length);
        }
    }

    public void WinLevel()
    {
        foreach (var bunker in _bunkers)
        {
            bunker.ResetBunkerLives();
        }
    }
    public void LoseLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

