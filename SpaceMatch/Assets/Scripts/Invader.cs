using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : Entity
{
    public SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    protected override void Death()
    {
        base.Death();
    }
}
