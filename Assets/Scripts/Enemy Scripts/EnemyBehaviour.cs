using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Vectors and Floats
    // Can set 2 positions for the enemy to move between which allows much more reusability
    [SerializeField] Vector3 position1;
    [SerializeField] Vector3 position2;
    private float speed = 0.3f;

    // Handles enemy movement
    void Update()
    {
        // Math pingpong allows for the transform to move between two set points
        transform.position = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * speed, 1));
    }
}   

