using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class MushScript : MonoBehaviour
{
    public dtZone attackZone;
    public float speed = 3f;
    public float walkStopRate = 0.04f;
    Rigidbody2D rb;

    public enum walkDirection { Right, Left}

    private walkDirection wb;
    private Vector2 WalkDirectionVector = Vector2.right;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public walkDirection wbDirection
    {
        get { return wb; }
        set { 
            if(wb != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value  == walkDirection.Right )
                {
                    WalkDirectionVector = Vector2.right;
                }else if (value == walkDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            wb = value; }
    }

    public bool _hasTarget = false;
    public bool hasTarget { get { return _hasTarget; } 
        private set {
            animator.SetBool(AnimationStrings.hasTarget, value);
            _hasTarget = value; 
        } 
    
    }

    public bool CanMove
    {
        get 
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }


    private void Update()
    {
        hasTarget = attackZone.colliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(speed * WalkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        } 
    }

    private void FlipDirection()
    {
        if (wbDirection == walkDirection.Right)
        {
            wbDirection = walkDirection.Left;
        } else if (wbDirection == walkDirection.Left)
        {
            wbDirection = walkDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
