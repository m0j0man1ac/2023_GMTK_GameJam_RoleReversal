using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using System.Threading.Tasks;
using System.Linq;

public class DialougeManagerScriptv2 : MonoBehaviour
{
    public static DialougeManagerScriptv2 instance;

    public Transform dialougeParent;
    public GameObject dialougeBox;
    public List<Transform> activeDialouges = new List<Transform>();
    
    private RectTransform mostRecent;
    private float mostRecentHeight;

    public float spacing = 20;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (mostRecent == null) return;
        if(mostRecentHeight != mostRecent.rect.height)
        {
            mostRecentHeight = mostRecent.rect.height;
            UpdatePositions();
        }
    }

    public void UpdatePositions()
    {
        activeDialouges[activeDialouges.Count-1].localPosition = Vector3.zero;

        float height = mostRecentHeight;
        for (int i = 1; i < activeDialouges.Count; i++)
        {
            var idx = activeDialouges.Count - i-1;

            activeDialouges[idx].DOKill();
            activeDialouges[idx].DOLocalMoveY(height + (spacing*i), .1f);

            height += activeDialouges[idx].GetComponent<RectTransform>().rect.height;
        }
    }

    public void StartTextBubble(Card card)
    {
        var sentence = "There should of been dialouge here";
        if (card.dialougeGroup) sentence = card.dialougeGroup?.dialouges.GrabRandom();
        StartTextBubble(sentence);
    }

    public void StartTextBubble(string dialouge)
    {
        var tempSpeech = GameObject.Instantiate(dialougeBox, dialougeParent);
        activeDialouges.Add(tempSpeech.transform);
        UpdatePositions();

        dialougeParent.DOKill();
        dialougeParent.DOJump(dialougeParent.position, .3f, 1, .4f).SetEase(Ease.OutElastic);

        FinishOtherDialouge();
        var reader = tempSpeech.GetComponent<TextReader>();
        reader.StartDialouge(dialouge);
        reader.parent = this;
        mostRecent = tempSpeech.GetComponent<RectTransform>();
    }

    public async void FinishOtherDialouge()
    {
        if (activeDialouges.Count < 2) return;

        var dialouge = activeDialouges[activeDialouges.Count - 2];
        var reader = dialouge.GetComponent<TextReader>();
        reader.StopAllCoroutines();
        reader.objectText.text = reader.text;

        if (reader.activeFade != null) await reader.activeFade;
        else await reader.FadeOut();
        //activeDialouges.Remove(dialouge);
        //Destroy(dialouge.gameObject);
    }

    public void RemoveDialouge(Transform t)
    {
        Debug.Log("removing dialouge " + t);
        activeDialouges.Remove(t);
        GameObject.Destroy(t.gameObject);
        UpdatePositions();
    }
}
