using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWorking : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueTrigger.dialogueOption = DialogueOption.Attacking;
            dialogueTrigger.TriggerDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueTrigger.dialogueOption = DialogueOption.Debuff;
            dialogueTrigger.TriggerDialogue();
        }
    }
}
