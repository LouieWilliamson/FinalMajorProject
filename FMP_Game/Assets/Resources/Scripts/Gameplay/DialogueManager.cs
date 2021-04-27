using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    public Animator anim;
    private bool isTyping;
    private string currentSentence;
    public float timeTilNextLetter;

    private bool playerSaved;
    private PlayerMovement player;
    private AudioManager sound;
    void Start()
    {
        sentences = new Queue<string>();
        isTyping = false;
        playerSaved = false;
        sound = GameObject.FindGameObjectWithTag("Manager").GetComponent<AudioManager>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        player.playerActive = false;
        player.ResetInput();

        anim.SetBool("DialogueActive", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && !isTyping)
        {
            EndDialogue();
            return;
        }

        //stops the player being able to continue before text is displayed
        if (!isTyping)
        {
            currentSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence));
        }
        else
        {
            SkipTextAnimation();
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            int chanceToPlaySFX = Random.Range(1, 5);
            if (chanceToPlaySFX == 1) sound.PlaySFX(AudioManager.SFX.DialogueSound);
            dialogueText.text += letter;
            isTyping = true;
            yield return new WaitForSeconds(timeTilNextLetter); ;
        }

        isTyping = false;
    }
    private void SkipTextAnimation()
    {
        isTyping = false;
        StopAllCoroutines();
        dialogueText.text = currentSentence;
    }
    private void EndDialogue()
    {
        player.playerActive = true;
        anim.SetBool("DialogueActive", false);
    }
    public void SavePlayer(GameObject p)
    {
        if (!playerSaved)
        {
            player = p.GetComponent<PlayerMovement>();
            playerSaved = true;
        }
    }
}
