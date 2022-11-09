using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Removed Script
// Infinite repeating background that follows the player
public class BackgroundRepeater : MonoBehaviour
{
    private float speed = 0.5f;
    private string sortLayer = string.Empty;
    public Material material;
    public MeshRenderer render;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().sortingOrder = 0;
    }

    void Update()
    {
        Vector2 backgroundOffset = new Vector2(Time.time * speed, 5);
        material.mainTextureOffset = backgroundOffset;
    }
}
