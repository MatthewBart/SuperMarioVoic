using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For displaying the player scores on game completion
public class HandleScore : MonoBehaviour
{
    // Score variable
    private int score;

    void Start()
    {
        score = GameObject.Find("Mario").GetComponent<PlayerMovement>().score;
    }

    // Prints score to screen
    private void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score.ToString();
    }


}
