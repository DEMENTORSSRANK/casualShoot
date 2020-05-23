using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Vector2 middlePos;

    public Bomb bombPrefab;

    public static Spawner Instance;

    private readonly float[] _waits =
    {
        5,
        4,
        3
    };

    private float CurWait => _waits[Levels.Instance.NowLevelIndex];
    
    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(CurWait, CurWait - 1f));

            for (var i = 0; i < Random.Range(1, 3); i++)
            {
                var bombObj = Instantiate(bombPrefab, transform);

                var pos = middlePos;

                pos.x += Random.Range(-13f, 13f);

                pos.y += Random.Range(-1f, 1.5f);

                bombObj.transform.position = pos;
            }
        }
    }
}
