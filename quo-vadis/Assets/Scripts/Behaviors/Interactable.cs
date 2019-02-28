using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Interactable : MonoBehaviour
{

    private DialogueData dialogue;

    public string dialoguePath;
    protected string[] dialogueOptions;
    protected int currentDialogueIndex;

    public UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogueIndex = 0;
        loadDialogueOptions(dialoguePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void loadDialogueOptions(string dialogueFilePath)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, dialogueFilePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            DialogueData loadedData = JsonUtility.FromJson<DialogueData>(dataAsJson);
            dialogueOptions = dialogue.DialogueOptions;
        }
        else
        {
            Debug.LogError("Unable to find dialogue options at: " + filePath);
        }
    }

    protected string GetCurrentDialogueOption()
    {
        return dialogueOptions[currentDialogueIndex];
    }

    protected string GetDialogueOption(int index)
    {
        return dialogueOptions[index];
    }

    protected void setDialogueOptionAsCurrent(int index)
    {
        currentDialogueIndex = index;
    }

    protected void setNextDialogueOptionAsCurrent()
    {
        currentDialogueIndex++;
    }

    protected void displayDialogue(string[] dialogueToDisplay)
    {
        UI.DisplayText(dialogueToDisplay);
    }

    protected void displayDialogue(string dialogueToDisplay)
    {
        UI.DisplayText(new string[] { dialogueToDisplay });
    }

    protected void displayNextSingleDialogue()
    {
        UI.DisplayText(new string[] { dialogueOptions[currentDialogueIndex] });
        currentDialogueIndex++;
    }

    protected void DisplayNextMultipleText(int numTextsToDisplay)
    {
        string[] arr = new string[numTextsToDisplay];
        for(int i = 0; i < numTextsToDisplay; i++)
        {
            ArrayUtility.Add(ref arr, dialogueOptions[currentDialogueIndex + i]);
        }
        UI.DisplayText(arr);
        currentDialogueIndex += numTextsToDisplay;
    }

}
