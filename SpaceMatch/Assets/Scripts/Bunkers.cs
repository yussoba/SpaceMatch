using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    public int bunkerStartLives = 5; 

    public GameObject[] bunkersPrefabs;

    private int _bunkerCurrentLives;

    private void Start()
    {
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
    }
}
