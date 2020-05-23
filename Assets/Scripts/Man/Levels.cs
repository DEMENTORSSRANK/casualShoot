using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public Text nowLevelText;
    
    public LevelObj[] levels;

    public LevelObj currentLevel;

    public static Levels Instance;

    private int _nowLevelIndex;

    public int NowLevelIndex
    {
        get => _nowLevelIndex;

        private set
        {
            _nowLevelIndex = value;

            nowLevelText.text = $"LEVEL {value + 1}";
            
            if (currentLevel != null)
                Destroy(currentLevel.gameObject);
            
            currentLevel = Instantiate(levels[value]);
            
            Values.ResetAll();
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }


    public void SetLevelIndex(int index)
    {
        if (NowLevelIndex >= 2)
            return;
        
        Audio.Instance.End(true);
        
        NowLevelIndex = index;
    }

    public void CompleteCurrentLevel()
    {
        NowLevelIndex++;
    }
    
}
