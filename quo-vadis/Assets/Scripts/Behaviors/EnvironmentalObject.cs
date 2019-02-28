using UnityEngine;
using System.Collections;

public class EnvironmentalObject : Interactable
{

    public SpriteRenderer sr;
    //public Animator anim; ???
    public CircleCollider2D interactRange;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if(other.gameObject.tag.Equals("Player") && Input.GetKeyUp(KeyCode.Space)) {
            //UI.DisplayText(dialogueOptions)
            Debug.DrawLine(transform.position, other.transform.position, Color.red, 1f);
        }
    }
}
