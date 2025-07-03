using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerAnimator : MonoBehaviour
{
    Animator _animator;
    Test_Player _player;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Test_Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_player.GetPlayerInputs().x != 0 || _player.GetPlayerInputs().y != 0)
        {
            _animator.SetBool("Move", true);

            SpriteDirectionChecker();
        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }

    public void Flip(float value)
    {
        if (value < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    void SpriteDirectionChecker()
    {
        if (_player.GetPlayerInputs().x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
