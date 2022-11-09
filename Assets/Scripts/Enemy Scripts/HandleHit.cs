using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles enemy and player collisions
public class HandleHit : MonoBehaviour
{
    public GameObject deathParticle;
    // Depending on what part of player touches the enemy
    // Either destroys the enemy or damages the player
    // Spawns a particle
    // Increases player score then makes them jump slightly
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "EnemyHeadHitBox")
        {
            if (collision.collider.CompareTag("Foot"))
            {
                Instantiate(deathParticle,transform.position, Quaternion.identity);
                GameObject.Find("Mario").GetComponent<PlayerMovement>().score += 1000;
                GameObject.Find("Mario").GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                Destroy(gameObject);
            }
        }
        else
        if (collision.otherCollider.name == "EnemyBodyHitBox")
        {
            if (collision.collider.name == "BodyHitBox")
            {
                GameObject.Find("Mario").GetComponent<PlayerMovement>().HandleHit();
            }
        }   
    }
}
