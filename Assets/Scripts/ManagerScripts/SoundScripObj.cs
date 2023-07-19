using PlasticGui.WorkspaceWindow;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace ScriptableObjects 
{

    [CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundScripObj : ScriptableObject
{
        private static readonly float SEMITONES_CONVERSION = 1.05946f; 
        public enum PlayOrder { in_order, reverse, random };

        #region config

        public AudioMixerGroup mixGroup;
        public AudioClip[] clips;
        [Range(0f,1f)] public float volMin = .5f, volMax = .5f;
        [Range(.1f,3f)] public float pitchMin = 1f, pitchMax = 1f;
        private int playIndex;
        public PlayOrder playOrder = PlayOrder.in_order;

        public bool useSemitones;
        [Range(-10,10)]public int semitoneMin, semitoneMax;

        #endregion

        #region PreviewCode
        private AudioSource previewer;

        private void OnEnable()
        {
            previewer = EditorUtility
                .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                    typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }

        public void PlayPreview()
        {
            Play(previewer);
        }

        public void StopPreview()
        {
            previewer.Stop();
        }

        #endregion

        public void SyncPitchAndSemitones()
        {
            if (useSemitones)
            {
                pitchMin = Mathf.Pow(SEMITONES_CONVERSION, semitoneMin);
                pitchMax = Mathf.Pow(SEMITONES_CONVERSION, semitoneMax);
            }
            else
            {
                semitoneMin = Mathf.RoundToInt(Mathf.Log10(pitchMin)/Mathf.Log10(SEMITONES_CONVERSION));
                semitoneMax = Mathf.RoundToInt(Mathf.Log10(pitchMax) / Mathf.Log10(SEMITONES_CONVERSION));
            }
        }

        public AudioClip GetClip()
        {
            var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

            switch (playOrder)
            {
                case PlayOrder.in_order:
                    playIndex = (++playIndex) % clips.Length;
                    break;
                case PlayOrder.reverse:
                    playIndex = (--playIndex) % clips.Length;
                    break;
                case PlayOrder.random:
                    playIndex = Random.Range(0, clips.Length);
                    break;
            }

            return clip;
        }

        public AudioSource Play(AudioSource audioSourceParam = null)
        {
            //Debug.Log("Trying to play sound: " + this);

            //null check error
            if(clips.Length == 0)
            {
                Debug.Log("missing sound clip");
                return null;
            }

            var source = audioSourceParam;
            if (source == null)
            {
                var _obj = new GameObject(this.ToString(), typeof(AudioSource));
                _obj.transform.parent = AudioManagerScript.instance.transform;
                source = _obj.GetComponent<AudioSource>();
            }

            //set config
            source.outputAudioMixerGroup = mixGroup;
            source.clip = GetClip();
            source.volume = Random.Range(volMin, volMax);
            source.pitch = useSemitones
                ? Mathf.Pow(SEMITONES_CONVERSION, Random.Range(semitoneMin, semitoneMax))
                : Random.Range(pitchMin, pitchMax);

            source.Play();

            if(source != previewer)
            {
                Destroy(source.gameObject, source.clip.length / source.pitch);
            }


            return source;
        }

        private void OnValidate()
        {
            SyncPitchAndSemitones();
        }
    }


}
