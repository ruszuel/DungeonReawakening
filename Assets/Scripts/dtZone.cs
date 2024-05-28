using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dtZone : MonoBehaviour
{
    public UnityEvent noMoreBlock;
    public List<Collider2D> colliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);

        if (colliders.Count <= 0 ) {
            noMoreBlock.Invoke();
        }
    }

}
