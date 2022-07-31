using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderVisual : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimeteSprite), animationTime, animationTime);
    }

    private void AnimeteSprite()
    {
        _animationFrame++;

        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = animationSprites[_animationFrame];
    }
}
