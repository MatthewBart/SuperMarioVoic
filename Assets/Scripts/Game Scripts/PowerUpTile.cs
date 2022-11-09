using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerUpTile : MonoBehaviour
{
    public Tilemap powerUpTiles;

    private void Start()
    {
        powerUpTiles = GetComponent<Tilemap>();
    }

    // Sets player score +1000 and increases their health
    // Removes the collided tile
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the player head collides with the tile
        // Otherwise the function will call when the player jumps on the tiles as normal
        if (collision.collider.CompareTag("Head"))
        {
            // Checks for all contacts and sets tile to null
            // The +0.2f is to find the tile slightly above the players head
            Vector3 hitPos = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPos.x = hit.point.x + 0.2f;
                hitPos.y = hit.point.y + 0.2f;
                powerUpTiles.SetTile(powerUpTiles.WorldToCell(hitPos), null);
                GameObject.Find("Mario").GetComponent<PlayerMovement>().HandlePowerUp();
            } 
        }
    }
}
