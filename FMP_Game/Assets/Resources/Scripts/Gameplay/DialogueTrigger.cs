using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool SelfDelete;
    private GameObject player;
    public void TriggerDialogue()
    {
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        dm.SavePlayer(player);
        dm.StartDialogue(dialogue);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            TriggerDialogue();
            if (SelfDelete) Destroy(this.gameObject);
        }
    }
}
