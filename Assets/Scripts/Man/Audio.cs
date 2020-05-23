using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    public AudioClip click;

    public AudioClip explosion;

    public AudioClip lose;

    public AudioClip win;

    public AudioClip shoot;


    private AudioSource Source => GetComponent<AudioSource>();
    
    public Button[] buttons;

    public void Click()
    {
        Source.PlayOneShot(click);
    }

    public void Explosion()
    {
        Source.PlayOneShot(explosion);
    }

    public void End(bool isWin)
    {
        Source.PlayOneShot(isWin ? win : lose);
    }

    public void Shoot()
    {
        Source.PlayOneShot(shoot);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var b in buttons)
        {
            b.onClick.AddListener(Click);
        }
    }
}
