using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

public class DialogueEditor : EditorWindow
{

    public DialogueData dialogue;
    private string relativeFilePath = null;

    [MenuItem ("Window/Dialogue Editor")]
    static void Init()
    {
        DialogueEditor window = (DialogueEditor)EditorWindow.GetWindow(typeof(DialogueEditor));
        window.Show();
    }

    void OnGUI()
    {
        relativeFilePath = EditorGUILayout.TextField("FILE NAME", relativeFilePath);
        if (dialogue != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("dialogue");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save")) 
            {
                SaveDialogueData();
            }
        }

        if (GUILayout.Button("Load"))
        {
            LoadDialogueData(relativeFilePath);
        }

    }

    private void LoadDialogueData(string dialogueFilePath)
    {
        relativeFilePath = dialogueFilePath;
        string absoluteFilePath = Path.Combine(Application.streamingAssetsPath, dialogueFilePath);

        if (File.Exists(absoluteFilePath))
        {
            string dataAsJson = File.ReadAllText(absoluteFilePath);
            dialogue = JsonUtility.FromJson<DialogueData>(dataAsJson);

        }
        else
        {
            Debug.Log("Generated new Dialogue Data at:" + absoluteFilePath);
            dialogue = new DialogueData();
        }
    }

    private void SaveDialogueData()
    {
        string dataAsJson = JsonUtility.ToJson(dialogue);

        string absoluteFilePath = Path.Combine(Application.streamingAssetsPath, relativeFilePath);

        File.WriteAllText(absoluteFilePath, dataAsJson);
    }

}
