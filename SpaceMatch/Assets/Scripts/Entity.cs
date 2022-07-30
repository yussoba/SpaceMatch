using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Bullet>())
        {
            Death();
        }
    }
    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
