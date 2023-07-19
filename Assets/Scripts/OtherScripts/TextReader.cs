using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using ScriptableObjects;
using DG.Tweening;
using System.Threading.Tasks;

public class TextReader : MonoBehaviour
{
    public DialougeManagerScriptv2 parent;

    public TMP_Text objectText;
    public string text;
    public float lettersPerSecond = 20;

    //public string[] sounds;
    public SoundScripObj textSFX;

    // Start is called before the first frame update
    void Start()
    {
        objectText = GetComponentInChildren<TMP_Text>();
        //objectText.text = "";

        //StartDialouge(text);
    }

    public void StartDialouge(string sentence)
    {
        text = sentence;
        StartCoroutine(TypeSentence(sentence));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        //var layElem = GetComponentInChildren<LayoutElement>();
        //var prefWidth = layElem.preferredWidth;
        //layElem.preferredWidth = 10;

        objectText.text = "";
        foreach(char letter in sentence)
        {
            objectText.text += letter;
            textSFX.Play();
            //layElem.preferredWidth = Mathf.Clamp(layElem.preferredWidth + 15, 0, prefWidth);
            yield return new WaitForSeconds(1/lettersPerSecond);
        }

        activeFade = FadeOut();
        yield return null;
    }

    public Task activeFade;

    public async Task FadeOut()
    {
        //GetComponentInChildren<LayoutElement>().preferredWidth=700;
        await Task.Delay(2*1000);

        GetComponent<Outline>().enabled = false;
        bool done = false;
        var image = transform.GetComponent<Image>();
        var alpha = image.color.a;

        DOTween.To(() => alpha, x => alpha = x, 0, .5f)
            .OnComplete(() => { done = true; });
        
        while (!done)
        {
            var iColor = image.color;
            iColor.a = alpha;
            image.color = iColor;

            var tColor = objectText.color;
            tColor.a = alpha;
            objectText.color = tColor;

            await Task.Yield();
        }

        Debug.Log(transform + " dialouge finished fading");
        transform.DOKill();
        parent?.RemoveDialouge(transform);
    }
}
