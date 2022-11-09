using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

// Handles in game voice commands 
// Defines grammar for in game and creates actions
public class GameGrammar : MonoBehaviour
{
    // Variables
    private UIManager menuControl;
    private GameObject mario;
    private GrammarRecognizer gr;
    private string message;


    // Action is in System, using System; or System.Action
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Defines actions
    // Starts grammar recogniser
    private void Start()
    { 
        // Actions and defining the UI Manager
        menuControl = gameObject.GetComponent<UIManager>();
        actions.Add("run", Run);
        actions.Add("walk", Walk);
        actions.Add("left", Left);
        actions.Add("right", Right);
        actions.Add("jump", Jump);
        actions.Add("enter", Enter);
        actions.Add("pause", Pause);
        actions.Add("tutorial", Tutorial);
        actions.Add("return", Return);
        actions.Add("quit", Tutorial);
        actions.Add("replay", Replay);
        actions.Add("menu", Menu);
        actions.Add("stop", Stop);

        mario = GameObject.Find("Mario");
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                                                "GameOptions.xml"),
                                    ConfidenceLevel.Low);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
    }

    // Handling recognised words and invoking the corresponding action
    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;
        // use foreach to get all the meanings.
        foreach (SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message = valueString;
           
        }
        // use a string builder to create the string and out put to the user
        Debug.Log(message);
        actions[message].Invoke();
    }

    // Failsafe to disable the grammar recogniser on application quit
    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }


    // Action methods
    // Changes speed to run speed in the PlayerMovement script
    private void Run()
    {
        mario.GetComponent<PlayerMovement>().isWalking = false;
        mario.GetComponent<PlayerMovement>().isRunning = true;
        mario.GetComponent<PlayerMovement>().ChangeSpeed();
    }

    // Changes speed to the defined walk speed in the PlayerMovement script
    private void Walk()
    {
        mario.GetComponent<PlayerMovement>().isRunning = false;
        mario.GetComponent<PlayerMovement>().isWalking = true;
        mario.GetComponent<PlayerMovement>().ChangeSpeed();
    }


    // Both Left() and Right() handle the direction the player moves
    private void Left()
    {
        mario.GetComponent<PlayerMovement>().SetDirection("left");
    }

    private void Right()
    {
        mario.GetComponent<PlayerMovement>().SetDirection("right");
    }

    // Causes the player to jump
    private void Jump()
    {
        mario.GetComponent<PlayerMovement>().Jump();
    }

    // Enters the pipe if the proper conditions are met
    private void Enter()
    {
        mario.GetComponent<PlayerMovement>().EnterPipe();
    }

    // Pauses the game
    private void Pause()
    {
        menuControl.PauseGame();
    }

    // Enables the tutorial menu
    private void Tutorial()
    {
        menuControl.Tutorial();
    }

    // Exits the tutorial or unpauses the game if tutorial is not enabled
    private void Return()
    {
        if (menuControl.GetComponent<UIManager>().paused == true && menuControl.GetComponent<UIManager>().tutorialEnabled == false)
        {
            menuControl.Resume();
        }
        else
        {
            menuControl.Tutorial();
        }
    }

    // Quits the game
    private void Quit()
    {
        menuControl.Exit();
    }

    // Restarts the game
    private void Replay()
    {
        menuControl.Play();
    }

    // Ceases player movement
    private void Stop()
    {
        mario.GetComponent<PlayerMovement>().isRunning = false;
        mario.GetComponent<PlayerMovement>().isWalking = false;
        mario.GetComponent<PlayerMovement>().ChangeSpeed();
    }

    // Returns the player to menu
    private void Menu()
    {
        menuControl.Menu();
    }
}
