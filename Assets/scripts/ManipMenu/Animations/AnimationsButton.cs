﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsButton : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        GetComponentInParent<ManipMenu>().newActive(ManipMenu.View.Animations);
    }
}
