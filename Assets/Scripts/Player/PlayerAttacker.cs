using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttacker : Player
{
    private bool _canSword = true;

    private List<Bomb> AllBombs => FindObjectsOfType<Bomb>().ToList();
    
    public void AnimRocket(float posX)
    {
        Animator.SetTrigger("shoot");

        Sr.flipX = posX < transform.position.x;
    }

    public static void Damage()
    {
        Values.Damage(50);
    }

    public void AttackSword()
    {
        if (!_canSword)
            return;
        
        Animator.SetTrigger("sword");

        StartCoroutine(CheckingSword());

        if (AllBombs.Any(x => Vector2.Distance(x.transform.position, Position) < 5))
        {
            var near = AllBombs.Find(x =>
                Vector2.Distance(x.transform.position, Position) <=
                AllBombs.Select(y => Vector2.Distance(y.transform.position, Position)).Min());
            
            near.Boom();
        }
            
    }
    
    private void FixedUpdate()
    {
        CheckPress();
    }

    private static void CheckPress()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount >= 1)
            AttackRocket.Instance.SendRocket();
    }

    private IEnumerator CheckingSword()
    {
        _canSword = false;
        
        yield return new WaitForSeconds(.6f);

        _canSword = true;
    }
}
