using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public float timer = 0f;
    private string[] sentences;
    public TMP_Text text;
    public string textS;
    public Dialogue AttackingDialogues;
    public Dialogue DebuffDialogues;
    public Dialogue MonoDialogues;
    public Dialogue ShieldDialogues;
    public Dialogue PlayerAttackedDialogues;
    public Dialogue HeroLowDialogues;
    public Dialogue HeroMidDialogues;
    public Dialogue HeroChadDialogues;

    public DialogueTrigger dialogueTrigger;


    public float lettersPerSecond = 20;

    public string[] sounds;

    void Start()
    {
       dialogueBox.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
           dialogueBox.SetActive(false);
        }
    }

    public void StartDialoguePre(string sentence)
    {
        timer = 0;
        StartCoroutine(TypeSentence(textS));
    }

    public void StartDialogue(string[] dialogueSentences)
    {
        text.text = "";
        sentences = new string[dialogueSentences.Length];
        for (int i = 0; i < dialogueSentences.Length; i++)
        {
            sentences[i] = dialogueSentences[i];
        }
        textS = sentences[UnityEngine.Random.Range(0, dialogueSentences.Length)];
        StartDialoguePre(textS);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueBox.SetActive(true);
        foreach (char letter in sentence)
        {
            text.text += letter;
            AudioManagerScript.instance.PlaySoundRandomPitch(sounds[UnityEngine.Random.Range(0, sounds.Length)]);
            yield return new WaitForSeconds(1 / lettersPerSecond);
            timer = 0;
        }

        yield return null;
    }

    void EndDialogue()
    {

    }
}
