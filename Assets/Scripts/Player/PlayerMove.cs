using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Player
{
    public float moveSpeed = 3;

    public float jumpForce = 7;

    public ContactFilter2D groundFilter;

    private int _indexMove = -1;

    private bool IsMoveNow => _indexMove != -1;

    private bool IsLeftMove => _indexMove == 0;
    
    public bool IsGrounded => Rb.IsTouching(groundFilter) && Mathf.Abs(Rb.velocity.y) < .1f;
    
    public void SetIndexMove(int index)
    {
        _indexMove = index;
        
        Animator.SetBool("isMove", IsMoveNow);
        
        if (!IsMoveNow)
            return;
        
        Sr.flipX = IsLeftMove;
    }

    public void Jump()
    {
        if (!IsGrounded)
            return;
        
        Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        CheckMove();
        
        if (Position.y < -10)
            Game.Instance.GameOver();
    }

    private void CheckMove()
    {
        Animator.SetBool("isGrounded", IsGrounded);
    
        if (!IsMoveNow)
            return;

        var direction = IsLeftMove ? Vector2.left : Vector2.right;

        direction *= moveSpeed * Time.deltaTime;

        Rb.position += direction;
    }
}
