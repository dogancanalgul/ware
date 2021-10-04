using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMouse : MonoBehaviour
{

    public MouseUser mouseUser = null;
    public Action OnChange;

    private int buttonMode = 0;
    private MouseUser oldUser;//to allow middle button movements

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseUser == null)
            return;

        if (Input.GetMouseButtonDown(buttonMode))
        {
            mouseUser.mouseDown();
        }
        else if (Input.GetMouseButton(buttonMode))
        {
            mouseUser.keepDown();
        }
        else if (Input.GetMouseButtonUp(buttonMode))
        {
            mouseUser.mouseUp();
        }    
    }

    public void changeUser(MouseUser mouseUser)
    {
        if (this.mouseUser != null)
            this.mouseUser.disengage();
        this.mouseUser = mouseUser;
        if (OnChange != null)
            OnChange();
    }
    public void changeUser(MouseUser mouseUser, int buttonMode, bool saveload)
    {

        if (this.mouseUser != null)
            this.mouseUser.disengage();

        if (saveload){// true is save
            if (this.buttonMode == 0)
                oldUser = this.mouseUser;
            this.mouseUser = mouseUser;
            this.buttonMode = buttonMode;
            this.buttonMode = buttonMode;
            mouseUser.mouseDown();
        }
        else if(buttonMode == this.buttonMode)
        {
            this.mouseUser = oldUser;
            this.buttonMode = 0;
        }

        if (OnChange != null)
            OnChange();
    }
}
