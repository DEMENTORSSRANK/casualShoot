using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj : MonoBehaviour
{
    public Vector2 startPoint;

    private void Start()
    {
        Player.Position = startPoint;
    }
}
