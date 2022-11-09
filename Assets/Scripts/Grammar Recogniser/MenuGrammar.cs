using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

// Handles menu grammar using actions and XML grammar
public class MenuGrammar : MonoBehaviour
{
    // For calling functions and handling Grammar
    private UIManager menuControl;
    private GrammarRecognizer gr;
    private string message;

    // Action is in System, using System; or System.Action
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Defines the grammar path and starts the grammar recogniser
    // Creates actions
    private void Start()
    {
        menuControl = gameObject.GetComponent<UIManager>();

        actions.Add("ok", Ok);
        actions.Add("begin", Begin);
        actions.Add("tutorial", Tutorial);
        actions.Add("quit", Quit);
        actions.Add("return", Tutorial);

        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                                                "MenuOptions.xml"),
                                    ConfidenceLevel.Low);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
    }

    // For handling when a valid word is recognised
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
        Debug.Log(message);
        actions[message].Invoke();
    }

    // Failsafe to disable grammar recogniser when game exits
    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }

    // Action functions that make use of the UI Manager script
    private void Ok()
    {
        menuControl.Ok();
    }

    private void Begin()
    {
        menuControl.Play();
    }

    private void Tutorial()
    {
        menuControl.Tutorial();
    }

    private void Quit()
    {
        menuControl.Exit();
    }
}
