using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public TextMeshProUGUI livesUI;
    public Bullet bulletPrefab;
    public GameObject[] lifesUI;
    public float speed = 5.0f;
    public int playerCurrentLives;
    public int playerStartLives = 3;

    private GameManager _gameManager;
    private bool _bulletActive;
    
    public void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        playerCurrentLives = playerStartLives;
        livesUI.text = playerCurrentLives.ToString();
    }
    public void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector3.left); 
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_bulletActive)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.destroyed += BulletDestroyed;
            _bulletActive = true;
        } 
    }

    private void BulletDestroyed()
    {
        _bulletActive = false;
    }

    public override void Death()
    {
        if (playerCurrentLives > 0)
        {
            if (playerCurrentLives > 1)
            {
                lifesUI[playerCurrentLives - 2].SetActive(false);
            }
            playerCurrentLives = playerCurrentLives - 1;
            livesUI.text = playerCurrentLives.ToString();
        }
        if (playerCurrentLives <= 0)
        {
            base.Death();
            _gameManager.LoseLevel();
        }  
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
