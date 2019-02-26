using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private DialogueData dialogue;

    private string[] dialogueOptions;
    private int currentDialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogueIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadDialogueOptions(string dialogueFilePath)
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


}
