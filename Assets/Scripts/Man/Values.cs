using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Values : MonoBehaviour
{
    public static Values Instance;

    public Text healthText;

    public Text armorText;

    public Text killsText;

    public Text bestKillSText;
    
    public int startHealth = 100;

    public int startArmor = 100;

    public int needKillsToComplete = 50;

    private int _nowHealth;

    private int _nowArmor;

    private int _nowKills;

    public int HealthNow
    {
        get => _nowHealth;
        private set
        {
            _nowHealth = value;

            healthText.text = $"{value}%";
            
            if (value <= 0)
                Game.Instance.GameOver();
        }
    }

    public int ArmorNow
    {
        get => _nowArmor;

        private set
        {
            _nowArmor = value;

            armorText.text = $"{value}%";
        }
    }

    public int KillsNow
    {
        get => _nowKills;

        private set
        {
            _nowKills = value;

            killsText.text = value.ToString();

            if (value > BestKills)
                BestKills = value;
        }
    }

    private int BestKills
    {
        get => PlayerPrefs.GetInt("best");

        set
        {
            PlayerPrefs.SetInt("best", value);

            bestKillSText.text = $"BEST KILLS: {value}";
        }
    }

    public static void AddKill()
    {
        Instance.KillsNow++;
        
        if (Instance.KillsNow >= Instance.needKillsToComplete)
            Levels.Instance.CompleteCurrentLevel();
    }

    public static void Damage(int value)
    {
        var damageArmor = Instance.ArmorNow > 0;

        if (damageArmor)
            Instance.ArmorNow -= value;
        else
            Instance.HealthNow -= value;
    }
    
    public static void ResetAll()
    {
        Instance.HealthNow = Instance.startHealth;

        Instance.ArmorNow = Instance.startArmor;

        Instance.KillsNow = 0;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BestKills = BestKills;
    }
}
