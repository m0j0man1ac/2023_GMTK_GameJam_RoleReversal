using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScriptableObjects;

public class AnimationSounds : MonoBehaviour
{
    public SoundScripObj[] sounds;

    public void PlaySound(int soundIdx)
    {
        sounds[soundIdx].Play();    
    }
}
