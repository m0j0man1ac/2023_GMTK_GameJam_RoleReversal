using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Bars : MonoBehaviour
{
    public enum BarType {courage, drama}
    public BarType barType;

    public static Bars courageInstance, dramaInstance;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int maxValue = 100;
    public int currentValue;

    private void Awake()
    {
        switch (barType)
        {
            case BarType.courage:
                courageInstance = this;
                break;
            case BarType.drama:
                dramaInstance = this;
                break;
        }
    }

    void Start()
    {
        setMaxValue(100);
    }
    

    public void setMaxValue(int value)
    {
        slider.value = 1;
        slider.maxValue = value;
    }

    public void setValue(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private float animSpeed = .4f;

    public void ChangeValue (int num)
    {
        if(num>0) this.transform.DOJump(transform.position, .5f, 1, animSpeed).SetEase(Ease.OutElastic);
        else this.transform.DOShakePosition(animSpeed * .5f, .5f).SetLoops(2).SetEase(Ease.OutElastic);
        
        currentValue += num;
        setValue(currentValue);

        HeroAnimation.instance.EvaluateState();
    }
}

