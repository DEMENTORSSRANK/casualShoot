using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public GameObject menu;

    public GameObject game;

    public GameObject over;
    
    private void Awake()
    {
        Instance = this;

        Screen.orientation = ScreenOrientation.Landscape;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;
    }

    private void Start()
    {
        menu.SetActive(true);
        
        game.SetActive(false);
    }

    public void StartGame(int level)
    {
        game.SetActive(true);
        
        menu.SetActive(false);
        
        Levels.Instance.SetLevelIndex(level);
        
        Spawner.Instance.StartSpawning();
    }

    public void GameOver()
    {
        over.SetActive(true);

        Audio.Instance.End(false);
        
        StartCoroutine(WaitGoMenu());
    }

    private IEnumerator WaitGoMenu()
    {
        Time.timeScale = 0;
        
        yield return new WaitForSecondsRealtime(2);
        
        GoMenu();
    }
    
    public void GoMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("game");
    }
}
