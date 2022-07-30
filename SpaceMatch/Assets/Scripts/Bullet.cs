using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    public Action destroyed;

    private void Update()
    {
        transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        destroyed();
        Destroy(gameObject);
    }
}
