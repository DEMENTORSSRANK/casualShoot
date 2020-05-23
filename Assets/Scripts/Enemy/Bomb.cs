using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    public Sprite[] deadAnim;

    public Sprite[] idleAnim;

    public float moveSpeed = 1.3f;

    private Vector2 _offsetToSet;

    private bool _isDead;

    private Vector2 ToGoPos => Player.Position + _offsetToSet;

    private void Start()
    {
        _offsetToSet.x += Random.Range(-.2f, .2f);

        _offsetToSet.y += Random.Range(-.2f, .2f);

        StartCoroutine(IdleAnim());
    }

    public void Boom()
    {
        if (_isDead)
            return;

        Audio.Instance.Explosion();
        
        Values.AddKill();
        
        GetComponent<CircleCollider2D>().enabled = false;
        
        StartCoroutine(DeadAnim());
    }

    private IEnumerator DeadAnim()
    {
        _isDead = true;
        
        foreach (var d in deadAnim)
        {
            GetComponent<SpriteRenderer>().sprite = d;
            
            yield return new WaitForSeconds(.03f);
        }
        
        Destroy(gameObject);
    }

    private IEnumerator IdleAnim()
    {
        while (true)
        {
            foreach (var a in idleAnim)
            {
                if (_isDead)
                    yield break;
                
                GetComponent<SpriteRenderer>().sprite = a;

                yield return new WaitForSeconds(.03f);
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (_isDead)
            return;

        transform.position = Vector2.MoveTowards(transform.position, ToGoPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        PlayerAttacker.Damage();
        
        Boom();
    }
}
