using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Kills player on collission
    // Calls the game over method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        GameObject.Find("GameController").GetComponent<UIManager>().GameOver();
    }
}
