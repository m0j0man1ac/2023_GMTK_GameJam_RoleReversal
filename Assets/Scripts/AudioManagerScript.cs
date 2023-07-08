using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManagerScript: MonoBehaviour
{
    public Sound[] activeMusic;

    public static AudioManagerScript instance;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        //SINGLETON
        //persist across scene loads
        if(instance==null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        //GENERATE AUDIO SOURCES As CHILDREN
        foreach(Sound s in sounds)
        {
            if (s.name.Length == 0) s.name = s.clip.name;

            GameObject temp = new GameObject(s.name);
            temp.transform.parent = transform;

            s.source = temp.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;

            s.source.outputAudioMixerGroup = s.audioMixGroup;
        }
    }

    //PLAYS SOUND WITH MATCHING NAME
    //SEARCHES THROUGH ARRAY
    public void PlaySound(string sound)
    {
        Sound s = null;
        foreach (Sound temp in sounds) if (temp.name.Equals(sound)) s = temp;
        //Debug.Log("Playing Sound " + sound);
        if (s.source != null) s.source.Play();
        else Debug.LogError("cant find sound: " + sound);
        return;
    }

    public void PlaySoundRandomPitch(string sound)
    {
        Sound s = null;
        foreach (Sound temp in sounds) if (temp.name.Equals(sound)) s = temp;

        //null check
        if(s.source==null)
        {
            Debug.LogError("cant find sound: " + sound);
            return;
        }

        //change pitch, play, reset pitch
        s.source.pitch = s.pitch + UnityEngine.Random.Range(-.2f,.2f);
        if (s.source != null) s.source.Play();
        //s.source.pitch = s.pitch;
    }

    public void StartSoundLoop(string sound, float time)
    {
        StartCoroutine(PlayLoopForSeconds(sound, time));
    }

    public IEnumerator PlayLoopForSeconds(string soundName, float time)
    {
        Sound s = null;
        foreach (Sound temp in sounds) if (temp.name.Equals(soundName)) s = temp;

        //null check
        if (s.source == null)
        {
            Debug.LogError("cant find sound: " + soundName);
            yield break;
        }

        s.source.loop = true;
        s.source.Play();

        yield return new WaitForSecondsRealtime(time);
        s.source.loop = false;
        s.source.Stop();

        yield break;
    }

    public void PlaySoundDelaySeconds(string soundName, float delayTime)
    {
        StartCoroutine(PlaySoundDelaySecondsCoroutine(soundName, delayTime)) ;
    }

    //PLAYS SOUNDS AFTER FLOAT SECONDS
    IEnumerator PlaySoundDelaySecondsCoroutine(string soundName, float delayTime)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        PlaySound(soundName);
    }
}
