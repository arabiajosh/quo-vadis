using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text textCanvas;

    public Animation anim;
    // Use this for initialization
    void Start()
    {

    }

    public void DisplayText(string[] textsToDisplay)
    {
        StartCoroutine(DisplayMultipleText(textsToDisplay));
    }

    private IEnumerator DisplayMultipleText(string[] texts) {

        textCanvas.enabled = true;
        int i = 0;
        while (i < texts.Length) {
            textCanvas.text = texts[i];
            do
            {
                yield return null;
            }
            while (!Input.GetKeyUp(KeyCode.Space));
            i++;
        }
        textCanvas.enabled = false;
    }
}
