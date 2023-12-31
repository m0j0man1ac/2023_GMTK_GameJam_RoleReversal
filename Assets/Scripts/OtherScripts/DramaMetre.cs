using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class DramaMetre : MonoBehaviour
{
    public static DramaMetre instance;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int maxDrama = 100;
    public int currentDrama;
    
    void Awake()
    {
        setMaxDrama(100);
        instance=this;
    }
    

    public void setMaxDrama(int drama)
    {
        slider.value = 0;
        slider.maxValue = drama;
    }

    public void setDrama(int drama)
    {
        slider.value = drama;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private float animSpeed = .4f;

    public void increaseDrama (int num)
    {
        this.transform.DOJump(transform.position, .5f, 1, animSpeed).SetEase(Ease.OutElastic);

        currentDrama += num;
        setDrama(currentDrama);
        
    }

    public void decreaseDrama (int num)
    {
        this.transform.DOShakePosition(animSpeed*.5f, .5f).SetLoops(2).SetEase(Ease.OutElastic);

        currentDrama -= num;
        setDrama(currentDrama);
    }


}
