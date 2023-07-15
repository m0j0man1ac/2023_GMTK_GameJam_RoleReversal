using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HealthManagerScript : MonoBehaviour
{
    public static HealthManagerScript instance;

    public int VillainMaxHealth = 999, villainHealth;
    public int HeroMaxHealth = 60, heroHealth;

    public TMP_Text UIVillainHealth, UIVillainMaxHealth;
    public TMP_Text UIHeroHealth, UIHeroMaxHealth;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        villainHealth = VillainMaxHealth;
        heroHealth = HeroMaxHealth;

        UpdateHealthUI();
    }

    public void VillainDamage(int value)
    {
        villainHealth -= value;
        UpdateHealthUI();
    }

    public void HeroDamage(int value)
    {
        heroHealth -= value;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        UIVillainHealth.text = villainHealth.ToString();
        UIVillainMaxHealth.text = VillainMaxHealth.ToString();

        UIHeroHealth.text = heroHealth.ToString();
        UIHeroMaxHealth.text = HeroMaxHealth.ToString();
    }
}
