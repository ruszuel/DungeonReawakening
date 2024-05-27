using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float offset = 1f; // Ensure offset is not zero to avoid division by zero
    private Vector2 startPosition;
    private float newXposition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure offset is not zero to prevent NaN issues
        if (offset != 0)
        {
            newXposition = Mathf.Repeat(Time.time * -moveSpeed, offset);

            // Create the new position as a Vector3 to maintain the z value
            Vector3 newPosition = startPosition + Vector2.right * newXposition;

            // Assign the new position
            transform.position = newPosition;
        }
        else
        {
            Debug.LogError("Offset cannot be zero.");
        }
    }
}


