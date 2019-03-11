using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Interactable : MonoBehaviour
{
    private DialogueData dialogue;

    private bool playerTrigger;

    public string dialoguePath;
    protected string[] dialogueOptions;
    protected int currentDialogueIndex;

    public UIManager UI;

    public Animator anim;

    private bool end;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("displayDialogue", false);
        end = false;
        currentDialogueIndex = 0;
        loadDialogueOptions(dialoguePath);
        playerTrigger = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Z) && !end){
            if(playerTrigger == true && !end){
                anim.SetBool("displayDialogue", true);
                Debug.Log(currentDialogueIndex);
                displayDialogue(GetCurrentDialogueOption());
                setNextDialogueOptionAsCurrent();
            }
        }
        else if(end){
            if(Input.GetKeyDown(KeyCode.Z))
                dialogueEnd();
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        playerTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other){
        playerTrigger = false;
    }

    protected void loadDialogueOptions(string dialogueFilePath)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, dialogueFilePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            DialogueData dialogue = JsonUtility.FromJson<DialogueData>(dataAsJson);
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
        if(currentDialogueIndex >= dialogueOptions.Length)
            end = true;
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

    protected void dialogueEnd(){
        end = false;
        anim.SetBool("displayDialogue", false);
        currentDialogueIndex = 0;
    }

}
