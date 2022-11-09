using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player movement
// This includes player movement, managing tile teleportation and dealing with health management
public class PlayerMovement : MonoBehaviour
{ 
    // Variables
    public int score = 0;
    private int health = 1;
    private float movementSpeed;

    // Bools for handling player input
    public bool isWalking;
    public bool isRunning;
    public bool left;
    public bool right;
    public bool enterZone1;
    public bool enterZone2;

    // For handling animations
    private Animator animator;

    // Defined GameObjects for pipe teleporting and for showing player Health
    public GameObject exitZone;
    public GameObject exitZone2;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject headHitBox;
    public GameObject bodyHitBox;
    public GameObject deathParticle;

    private void Start()
    {
        // Starts the player in idle animation
        animator = GetComponent<Animator>();
        animator.SetBool("isStopped", true);
    }

    void Update()
    {
        // Handling left and right movement
        HandleMovement();
    }

    // Handles movement direction
    private void HandleMovement()
    {
        // Alters player movement direction depending on player input
        if (left == true)
        {
            transform.position += transform.right * - movementSpeed * Time.deltaTime;
        }
        else if(right == true)
        {
            transform.position += -transform.right * - movementSpeed * Time.deltaTime;
        }
    }

    // Changes speed depending on player input
    public void ChangeSpeed()
    {
        // Changes player animation if movement if walking/running is occuring
        if(isWalking == true)
        {
            movementSpeed = 1f;
            animator.SetBool("isStopped", false);
        }
        else if(isRunning == true)
        {
            movementSpeed = 2f;
            animator.SetBool("isStopped", false);
        }
        else
        {
            movementSpeed = 0f;
            animator.SetBool("isStopped", true);
        }
    }

    // Sets player direction
    public void SetDirection(String leftOrRight)
    {
        if(leftOrRight == "left")
        {
            left = true;
            right = false;
        }
        else if(leftOrRight == "right")
        {
            right = true;
            left = false;
        }
    }

    // Handles player jumping
    // Uses the AddForce method to simulate Mario styled jumping
    // This also allows for jumping in current direction
    public void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }

    // Handles pipe teleporting depending on where the player is standing
    public void EnterPipe()
    {
        if (enterZone1 == true)
        {
            transform.position = exitZone.transform.position;
            enterZone1 = false;
        }
        else if (enterZone2 == true)
        {
            transform.position = exitZone2.transform.position;
            enterZone2 = false;
        }
    }

    // Handles health 
    public void HandlePowerUp()
    {
        health += 1;
        ChangeHealth();
        score += 1000;
    }

    // Handles player being hit
    public void HandleHit() 
    {
        // Reduces health then calls the change health method to handle the hearts being showed
        // Destroys gameobject when player has 0 health
        // Calls the GameOver method
        health -= 1;
        ChangeHealth();

        if (health <= 0)
        {
            Instantiate(deathParticle,transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject.Find("GameController").GetComponent<UIManager>().GameOver();
        }
        // Player becomes invincible for 3 seconds by disabling it's hitboxes
        StartCoroutine(IFrames());

    }

    // Handles player invincibility 
    private IEnumerator IFrames()
    {
        headHitBox.SetActive(false);
        bodyHitBox.SetActive(false);
        yield return new WaitForSeconds(3);
        headHitBox.SetActive(true);
        bodyHitBox.SetActive(true);
        Debug.Log("Waited 3 seconds");
    }

    // Handles the activation of hearts depending on how much health a player has
    private void ChangeHealth()
    {
        switch (health)
        {
            case 1:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 2:
                heart2.SetActive(true);
                heart3.SetActive(false);
                break;
            case 3:
                heart3.SetActive(true);
                break;
        }
    }
}
