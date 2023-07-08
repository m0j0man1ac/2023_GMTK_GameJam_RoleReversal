using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextReader : MonoBehaviour
{
    public TMP_Text objectText;
    public string text;
    public float lettersPerSecond = 20;

    public string[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        objectText = GetComponent<TMP_Text>();
        objectText.text = "";

        StartDialouge(text);
    }

    public void StartDialouge(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        objectText.text = "";
        foreach(char letter in sentence)
        {
            objectText.text += letter;
            AudioManagerScript.instance.PlaySoundRandomPitch(sounds[Random.Range(0,sounds.Length)]);
            yield return new WaitForSeconds(1/lettersPerSecond);
        }

        yield return null;
    }
}
