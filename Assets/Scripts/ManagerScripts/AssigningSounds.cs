using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssigningSounds : MonoBehaviour
{
    public SoundScripObj cardRifle;
    public SoundScripObj cardPlay;
    public SoundScripObj endTurn;

    private void Start() {AddSounds();}
    private void OnEnable(){AddSounds();}

    public void AddSounds()
    {
        GameManagerScript.instance.cardRifleDel += PlayCardRifleSound;
        GameManagerScript.instance.cardPlayDel += PlayCardPlaySound;
        GameManagerScript.instance.endTurnDel += PlayEndTurnSound;
    }

    private void OnDisable()
    {
        GameManagerScript.instance.cardRifleDel -= PlayCardRifleSound;
        GameManagerScript.instance.cardPlayDel -= PlayCardPlaySound;
        GameManagerScript.instance.endTurnDel -= PlayEndTurnSound;
    }

    public void PlayCardRifleSound() {cardRifle.Play();}
    public void PlayCardPlaySound() {cardPlay.Play(); }
    public void PlayEndTurnSound() { endTurn.Play();}
}
