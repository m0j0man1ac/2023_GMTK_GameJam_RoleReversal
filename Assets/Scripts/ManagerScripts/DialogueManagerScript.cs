using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjects;

public class DialogueManagerScript : MonoBehaviour
{
    public static DialogueManagerScript instance;

    public SoundScripObj heroSFX, villainSFX;

    public static DialogueOption dialogueOption;
    public string[] dialogueStrings;


    private bool firstDialogue = true;
    public GameObject dialogueBox;
    public float timer = 0f;
    private string[] sentences;
    public TMP_Text text;
    public string textS;
    public Dialogue Attacking;
    public Dialogue Debuff;
    public Dialogue Mono;
    public Dialogue Shield;
    public Dialogue PlayerAttacked;
    public Dialogue HeroLow;
    public Dialogue HeroMid;
    public Dialogue HeroChad;

    public float lettersPerSecond = 20;

    public string[] sounds;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dialogues = new Dialogue[]
        {
            Attacking,
            Debuff,
            Mono,
            Shield,
            PlayerAttacked,
            HeroLow,
            HeroMid,
            HeroChad
        };
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

    public void TriggerDialogue(DialogueOption dialogueOptionManager)
    {
        dialogueStrings = new string[dialogues[(int)dialogueOptionManager].sentences.Length];
        for(int i = 0; i < dialogueStrings.Length; i++)
        {
            dialogueStrings[i] = dialogues[(int)dialogueOptionManager].sentences[i];
        }
        StartDialogue(dialogueStrings);  
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
        if(firstDialogue)
        {
            textS = " " + sentences[UnityEngine.Random.Range(0, dialogueSentences.Length)];
            firstDialogue = false;
        }
        else
        {
            textS = sentences[UnityEngine.Random.Range(0, dialogueSentences.Length)];
        }
        StartDialoguePre(textS);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueBox.SetActive(true);
        foreach (char letter in sentence)
        {
            text.text += letter;
            //AudioManagerScript.instance.PlaySoundRandomPitch(sounds[UnityEngine.Random.Range(0, sounds.Length)], .1f);
            heroSFX?.Play();
            yield return new WaitForSeconds(1 / lettersPerSecond);
            timer = 0;
        }

        yield return null;
    }
}

public enum DialogueOption
{
    Attacking,
    Debuff,
    Mono,
    Shield,
    PlayerAttacked,
    HeroLow,
    HeroMid,
    HeroChad
}