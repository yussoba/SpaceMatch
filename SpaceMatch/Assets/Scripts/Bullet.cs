using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    public Action destroyed;

    public bool enemyBullet;

    private void Update()
    {
        transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemyBullet)
        {
            destroyed();
        }
        
        Destroy(gameObject);
    }
}
