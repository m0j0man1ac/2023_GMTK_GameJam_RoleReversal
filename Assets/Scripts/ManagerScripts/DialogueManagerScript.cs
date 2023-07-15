using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagerScript : MonoBehaviour
{
    public static DialogueManagerScript instance;

    public static DialogueOption dialogueOption;
    public string[] dialogueStrings;

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

    public void TriggerDialogue()
    {
        if(dialogueOption == DialogueOption.Attacking)
        {
            dialogueStrings = new string[Attacking.sentences.Length];
            for (int i = 0; i < Attacking.sentences.Length; i++)
            {
                dialogueStrings[i] = Attacking.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.Debuff)
        {
            dialogueStrings = new string[Debuff.sentences.Length];
            for (int i = 0; i < Debuff.sentences.Length; i++)
            {
                dialogueStrings[i] = Debuff.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.Mono)
        {
            dialogueStrings = new string[Mono.sentences.Length];
            for (int i = 0; i < Mono.sentences.Length; i++)
            {
                dialogueStrings[i] = Mono.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.Shield)
        {
            dialogueStrings = new string[Shield.sentences.Length];
            for (int i = 0; i < Shield.sentences.Length; i++)
            {
                dialogueStrings[i] = Shield.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.PlayerAttacked)
        {
            dialogueStrings = new string[PlayerAttacked.sentences.Length];
            for (int i = 0; i < PlayerAttacked.sentences.Length; i++)
            {
                dialogueStrings[i] = PlayerAttacked.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.HeroLow)
        {
            dialogueStrings = new string[HeroLow.sentences.Length];
            for (int i = 0; i < HeroLow.sentences.Length; i++)
            {
                dialogueStrings[i] = HeroLow.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.HeroMid)
        {
            dialogueStrings = new string[HeroMid.sentences.Length];
            for (int i = 0; i < HeroLow.sentences.Length; i++)
            {
                dialogueStrings[i] = HeroMid.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
        }
        if (dialogueOption == DialogueOption.HeroChad)
        {
            dialogueStrings = new string[HeroChad.sentences.Length];
            for (int i = 0; i < HeroChad.sentences.Length; i++)
            {
                dialogueStrings[i] = HeroChad.sentences[i].ToString();
            }
            StartDialogue(dialogueStrings);
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
            AudioManagerScript.instance.PlaySoundRandomPitch(sounds[UnityEngine.Random.Range(0, sounds.Length)], .1f);
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

