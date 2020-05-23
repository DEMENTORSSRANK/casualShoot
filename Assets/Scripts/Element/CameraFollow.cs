using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 offset;

    public float modifer = 2.2f;
    
    private void LateUpdate()
    {
        var playerPos = (Vector3) Player.Position;

        var myPos = transform.position;

        playerPos += (Vector3) offset;

        playerPos.z = myPos.z;

        var distance = Vector2.Distance(playerPos, myPos);

        var speed = distance * modifer * Time.deltaTime;

        var setPos = Vector3.MoveTowards(myPos, playerPos, speed);

        transform.position = setPos;
    }
}
