using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityMoveMouse : MonoBehaviour, MouseUser
{
    public float speed = 2f;
    private Vector3 origin = Vector3.zero;

    void Start()
    {
    }

void Update()
    {
        
    }

    public void keepDown()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - origin);
        pos = pos.x * Camera.main.transform.right + pos.y * Vector3.up;

        Camera.main.transform.position += -pos * speed;
        origin = Input.mousePosition;
     }
    public void mouseDown()
    {
        origin = Input.mousePosition;
    }

    public void mouseUp()
    {
    }
    public void disengage()
    {
        mouseUp();
    }
}
