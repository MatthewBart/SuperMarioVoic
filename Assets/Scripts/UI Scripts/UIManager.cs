using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles the games UI management
public class UIManager : MonoBehaviour
{
    // GameObjects
    public GameObject pause;
    public GameObject tutorial;
    public GameObject menu;
    public GameObject gameComplete;
    public GameObject gameOver;
    public GameObject initialTutorial;
    
    // Handling if game is paused or tutorial is called
    public bool paused;
    public bool tutorialEnabled;

    void Start()
    {
        // Handles menu activation depending on the current scene
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            pause.SetActive(false);
            tutorial.SetActive(false);
        }
        else
        {
            initialTutorial.SetActive(true);
            menu.SetActive(false);
            tutorial.SetActive(false);
        }
    }

    // Actives the pause game object
    // Sets timescale to 0 which prevents everything from moving
    public void PauseGame()
    {
        if(gameComplete.activeInHierarchy == false && gameOver.activeInHierarchy == false)
        {
            if (paused == false)
            {
                Time.timeScale = 0f;
                pause.SetActive(true);
                paused = true;
            }
        }   
    }

    // Deactivates the pause game object 
    // Sets timescale to 1 to resume the game
    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // Loads the mainmenu 
    public void Menu()
    {
        if(paused == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        else if(gameComplete.activeInHierarchy == true || gameOver.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    // If the current scene is the main menu it deactivates the menu and enables the tutorial menu
    // Calling this function again reverts this
    // If the game is paused it activates the tutorial menu and deactivates the pause menu
    // Calling it again reverts this
    public void Tutorial()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Menu" )
        {
            if(initialTutorial.activeInHierarchy == false)
            {
                if (tutorialEnabled == true)
                {
                    tutorialEnabled = false;
                    tutorial.SetActive(false);
                    menu.SetActive(true);
                }
                else if (tutorialEnabled == false)
                {
                    tutorialEnabled = true;
                    tutorial.SetActive(true);
                    menu.SetActive(false);
                }
            }
            
        }
        else
        {
            if (paused == true)
            {
                if(tutorialEnabled == false)
                {
                    tutorial.SetActive(true);
                    tutorialEnabled = true;
                    pause.SetActive(false);
                }
                else
                {
                    tutorial.SetActive(false);
                    tutorialEnabled = false;
                    pause.SetActive(true);
                }
                
            }
        }
    }

    // Used to restart the game/start a new game
    // Loads game scene
    // LoadSceneMode.Single is used to load only the scene and unload all other scenes
    public void Play()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Menu")
        {
            if (initialTutorial.activeInHierarchy == false && tutorialEnabled == false)
            {
                SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
            }
        }
        else if(gameComplete.activeInHierarchy == true || gameOver.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
        }
        
    }

    // Quits the game only if the the player is on the main menu
    public void Exit()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Menu" && initialTutorial.activeInHierarchy == false && tutorialEnabled == false)
        {
            Application.Quit();
        }
    }

    // Sets the gameOver menu to active
    public void GameOver()
    {
        pause.SetActive(false);
        tutorial.SetActive(false);
        gameOver.SetActive(true);
    }

    // Sets the gameComplete menu to active
    public void GameComplete()
    {
        pause.SetActive(false);
        tutorial.SetActive(false);
        gameComplete.SetActive(true);
    }

    // Disables the initial tutorial object and activates the menu
    public void Ok()
    {
        initialTutorial.SetActive(false);
        menu.SetActive(true);
    }

}
