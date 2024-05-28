using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public dtZone attackZone;
    AIDestinationSetter ds;
    GameObject player;
    public GameObject origPos;
    GameObject waypoint;
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        ds = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

       waypoint = Instantiate(origPos, transform.position, Quaternion.identity);
     
    }

    public bool _hasTarget = false;
    public bool hasTarget
    {
        get { return _hasTarget; }
        private set
        {
            animator.SetBool(AnimationStrings.hasTarget, value);
            _hasTarget = value;
        }

    }

    void Update()
    {
        hasTarget = attackZone.colliders.Count > 0;
        float distance =    Vector2.Distance(transform.position, player.transform.position);

        if(distance < 7)
        {
            ds.target = player.transform;
        }
        else
        {
            ds.target = waypoint.transform;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
