using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinTile : MonoBehaviour
{
    // Tilemap
    public Tilemap breakableTiles;

    private void Start()
    {
        breakableTiles = GetComponent<Tilemap>();
    }
    
    // Sets player score +100 
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
            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPos.x = hit.point.x + 0.2f;
                hitPos.y = hit.point.y + 0.2f;
                GameObject.Find("Mario").GetComponent<PlayerMovement>().score+=100;
                breakableTiles.SetTile(breakableTiles.WorldToCell(hitPos), null);
            }
        }
    }
}
