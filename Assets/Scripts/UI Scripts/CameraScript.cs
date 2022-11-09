using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Follows player position at a slight offset to allow the player to plan their actions in advance
    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector2(player.transform.position.x + 2f, player.transform.position.y + 3.2f);
        }
        
    }
}
