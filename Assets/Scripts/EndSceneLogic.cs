using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public enum EndSceneState { HeroDeath, LowCourage, LowDrama, LowBoth, Victory };


public class EndSceneLogic : MonoBehaviour
{
    public bool playBool = false;
    private bool playTracker = false;

    public TMP_Text Title, Subtitle, Body;

    public int barThreshold = 85;

    public EndSceneSlideText[] textGroups;

    void Start()
    {
        ChooseEndSceneText();   
    }

    public void ChooseEndSceneText()
    {
        var gSS = GameStateSaver.instance;
        EndSceneSlideText result = null;

        if (gSS.heroHealth == 0) result = FindSlide(EndSceneState.HeroDeath);
        else if (gSS.dramaVal > barThreshold && gSS.courageVal > barThreshold)
            result = FindSlide(EndSceneState.Victory);
        else if (gSS.dramaVal < gSS.courageVal) result = FindSlide(EndSceneState.LowDrama);
        else if (gSS.courageVal < gSS.dramaVal) result = FindSlide(EndSceneState.LowCourage);

        if (result != null) UpdateText(result);
    }

    public EndSceneSlideText FindSlide(EndSceneState target)
    {
        EndSceneSlideText result = null;
        foreach(var slide in textGroups)
        {
            if (slide.type != target) continue;

            result = slide;
            break;
        }

        return result;
    }

    public void UpdateText(EndSceneSlideText textSO)
    {
        Title.text = textSO.Title;
        Subtitle.text = textSO.SubTitle;
        Body.text = textSO.Body;
    }

    private void OnValidate()
    {
        if (playTracker == playBool) return;
        playTracker = playBool;
        ChooseEndSceneText();
    }
}
