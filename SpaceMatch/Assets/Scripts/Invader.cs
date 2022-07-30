using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : Entity
{
    public Color color;
    public SpriteRenderer sprite;
    public Tuple<int, int> position;
    public Action<Tuple<int, int>, Color> onDestroyed;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public override void Death()
    {
        onDestroyed(position, color);
        base.Death();
    }
}
