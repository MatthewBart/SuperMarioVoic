using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Simple Win Conditions script
// Calls the game complete method from UI Manager
// A seperate script so it can be adjusted for seperate levels
public class WinConditions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("GameController").GetComponent<UIManager>().GameComplete();
    }
}
