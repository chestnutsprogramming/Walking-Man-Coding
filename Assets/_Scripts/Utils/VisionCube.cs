using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCube :  MonoBehaviour
{
    public void move(direction d, float tileSize, Vector3 playerPos)
    {
        Vector3 newPos = playerPos;

        switch(d)
        {
            case direction.up:    newPos += new Vector3(0, tileSize, 0); break;
            case direction.down:  newPos += new Vector3(0, -tileSize, 0); break;
            case direction.left:  newPos += new Vector3(-tileSize, 0, 0); break;
            case direction.right: newPos += new Vector3(tileSize, 0, 0); break;
        }

        transform.position = newPos;
    }

    public void reset(Vector3 pos)
    {
        transform.position = pos;
    }

}