using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public DialogueOption dialogueOption;
    public string[] dialogueStrings;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueOption = DialogueOption.Attacking;
            TriggerDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueOption = DialogueOption.Debuff;
            TriggerDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dialogueOption = DialogueOption.Mono;
            TriggerDialogue();
        }

    }

    public void TriggerDialogue()
    {
        switch (dialogueOption)
        {
            case DialogueOption.Attacking:
                dialogueStrings = new string[dialogueManager.AttackingDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.AttackingDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.AttackingDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.Debuff:
                dialogueStrings = new string[dialogueManager.DebuffDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.DebuffDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.DebuffDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.Mono:
                dialogueStrings = new string[dialogueManager.MonoDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.MonoDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.MonoDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.Shield:
                dialogueStrings = new string[dialogueManager.ShieldDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.ShieldDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.ShieldDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.PlayerAttacked:
                dialogueStrings = new string[dialogueManager.PlayerAttackedDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.PlayerAttackedDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.PlayerAttackedDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.HeroLow:
                dialogueStrings = new string[dialogueManager.HeroLowDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.HeroLowDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.HeroLowDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.HeroMid:
                dialogueStrings = new string[dialogueManager.HeroMidDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.HeroMidDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.HeroMidDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            case DialogueOption.HeroChad:
                dialogueStrings = new string[dialogueManager.HeroChadDialogues.sentences.Length];
                for (int i = 0; i < dialogueManager.HeroChadDialogues.sentences.Length; i++)
                {
                    dialogueStrings[i] = dialogueManager.HeroChadDialogues.sentences[i].ToString();
                }
                dialogueManager.StartDialogue(dialogueStrings);
                break;
            default:
                Debug.LogError("Invalid dialogue option!");
                break;
        }
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
