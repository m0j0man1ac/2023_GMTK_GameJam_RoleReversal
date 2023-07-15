using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void increaseCourage (int num)
    {
        currentCourage += num;

        setCourage(currentCourage);
        
    }

    public void decreaseCourage (int num)
    {
        currentCourage -= num;

        setCourage(currentCourage);
    }
}
