using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class CourageMetre : MonoBehaviour
{
    public static CourageMetre instance;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int maxCourage = 100;
    public int currentCourage;
    
    void Start()
    {
        setMaxCourage(100);
        instance=this;
    }
    

    public void setMaxCourage(int courage)
    {
        slider.value = 1;
        slider.maxValue = courage;
    }

    public void setCourage(int courage)
    {
        slider.value = courage;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private float animSpeed = .4f;

    public void increaseCourage (int num)
    {
        this.transform.DOJump(transform.position, .5f, 1, animSpeed).SetEase(Ease.OutElastic);
        currentCourage += num;

        setCourage(currentCourage);
        
    }

    public void decreaseCourage (int num)
    {
        this.transform.DOShakePosition(animSpeed * .5f, .5f).SetLoops(2).SetEase(Ease.OutElastic);
        currentCourage -= num;

        setCourage(currentCourage);
    }
}

