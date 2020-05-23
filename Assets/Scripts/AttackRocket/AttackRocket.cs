using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRocket : MonoBehaviour
{
    public GameObject rocketPrefab;

    public GameObject crossHairPrefab;

    public float moveRocketSpeed = 6;
    
    public static AttackRocket Instance;

    private Vector2 _toGoPos;

    private Transform _currentRocket;

    private GameObject _currentCrossHair;

    private bool HaveRocket => _currentRocket != null;

    public void DestroyRocket()
    {
        Destroy(_currentRocket.gameObject);
            
        Destroy(_currentCrossHair);
    }
    
    public void SendRocket()
    {
        if (HaveRocket)
            return;
        
        

        _toGoPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var checkPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (checkPos.y <= .5f)
            return;

        if (Game.Instance.menu.activeSelf)
            return;
        
        Audio.Instance.Shoot();
        
        _currentRocket = Instantiate(rocketPrefab).transform;

        _currentCrossHair = Instantiate(crossHairPrefab, _toGoPos, Quaternion.identity);

        Player.Attacker.AnimRocket(_toGoPos.x);
        
        _currentRocket.transform.position = Player.Position;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckMoveRocket();
    }

    private void CheckMoveRocket()
    {
        if (!HaveRocket)
            return;

        var setPos = Vector2.MoveTowards(_currentRocket.position, _toGoPos, 
            moveRocketSpeed * Time.deltaTime);
        
        var angle = 0f;
         
        var relative = _currentRocket.InverseTransformPoint(_toGoPos);

        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;

        _currentRocket.Rotate(0, 0, -angle);

        _currentRocket.position = setPos;

        if (_currentRocket.position == (Vector3) _toGoPos)
        {
            DestroyRocket();
        }
    }
}
