using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HealthManagerScript : MonoBehaviour
{
    public static HealthManagerScript instance;

    public Transform heroHitPos, villainHitPos;

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
        Mathf.Clamp(villainHealth, 0, VillainMaxHealth);
        UpdateHealthUI();
    }

    public void HeroDamage(int value)
    {
        value = (int)(value*GameManagerScript.instance.villainAttackMult);

        HeroAnimation.instance?.Hit();
        PopUpText.instance.PopUp(heroHitPos.position, value.ToString());

        heroHealth -= value;
        Debug.Log("hellloooooo????????");
        heroHealth = Mathf.Clamp(heroHealth, 0, HeroMaxHealth);
        UpdateHealthUI();
        if (heroHealth == 0) GameManagerScript.instance.EndGame();
    }

    public void UpdateHealthUI()
    {
        UIVillainHealth.text = villainHealth.ToString();
        UIVillainMaxHealth.text = VillainMaxHealth.ToString();

        UIHeroHealth.text = heroHealth.ToString();
        UIHeroMaxHealth.text = HeroMaxHealth.ToString();
    }
}
