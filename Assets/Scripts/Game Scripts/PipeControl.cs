using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the pipe hitboxes for player transportation
public class PipeControl : MonoBehaviour
{
    // While the player is inside the triggered hitbox
    // The pipe zone is enabled(depending on which hitbox)
    // When Enter() is called by the player it teleports the player to the corresponding exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "BodyHitBox")
        {
            if (gameObject.name == "EnterZone 1")
            {
                GameObject.Find("Mario").GetComponent<PlayerMovement>().enterZone1 = true;
            }
            else if (gameObject.name == "EnterZone 2")
            {
                GameObject.Find("Mario").GetComponent<PlayerMovement>().enterZone2 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Mario")
        {
            if (gameObject.name == "EnterZone 1")
            {
                GameObject.Find("Mario").GetComponent<PlayerMovement>().enterZone1 = false;
            }
            else if (gameObject.name == "EnterZone 2")
            {
                GameObject.Find("Mario").GetComponent<PlayerMovement>().enterZone2 = false;
            }
        }
    }
}
