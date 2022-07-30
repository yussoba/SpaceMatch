using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : Entity
{
    public Color color;
    public SpriteRenderer sprite;
    public Tuple<int, int> matrixPosition;
    
    private Action<Tuple<int, int>, Color> _onDestroyed;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnStart(Action<Tuple<int, int>, Color> onDestroyed, Color color, Tuple<int, int> matrixPosition, Vector3 position)
    {
        _onDestroyed = onDestroyed;
        this.color = color;
        this.matrixPosition = matrixPosition;
        
        transform.position = position;
        sprite.color = color;
    }
    
    public override void Death()
    {
        _onDestroyed(matrixPosition, color);
        base.Death();
    }
}
