using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int bunkerStartLives = 5; 
    public GameObject[] bunkersPrefabs;

    private GameManager _gamemanager;
    private int _bunkerCurrentLives;

    private void Start()
    {
        _gamemanager = FindObjectOfType<GameManager>();
        _bunkerCurrentLives = bunkerStartLives;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Bullet>() && _bunkerCurrentLives > 0)
        {
            _bunkerCurrentLives--;
        }
        if (_bunkerCurrentLives <= 0)
        {
            gameObject.SetActive(false);
        }
        if (collider.gameObject.GetComponent<Invader>())
        {
            _gamemanager.LoseLevel();
        }
    }

    public void ResetBunkerLives()
    {
        _bunkerCurrentLives = bunkerStartLives;
        gameObject.SetActive(true);
    }
}
