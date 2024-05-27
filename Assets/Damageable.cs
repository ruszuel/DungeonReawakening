using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health < 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvinsible = false;
    private float timeSinceHit = 0f;
    public float invisibilityTime = 0.25f;

    public bool IsAlive { 
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isInvinsible) 
        {
            if(timeSinceHit > invisibilityTime)
            {
                isInvinsible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    private void Hit (int damage)
    {
        if(IsAlive && !isInvinsible)
        {
            Health -= damage;
            isInvinsible=true;
        }
    }
}
