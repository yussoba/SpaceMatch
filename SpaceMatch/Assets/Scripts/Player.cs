using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float speed = 5.0f;

    public Bullet bulletPrefab;

    private bool _bulletActive;

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
        base.Death();
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
