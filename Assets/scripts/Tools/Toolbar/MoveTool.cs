using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTool : MonoBehaviour, ToolbarTool
{

    public EntityMouse entityMouse;
    public EntityMoveMouse entityMoveMouse;

    // Start is called before the first frame update
    void Start()
    {
       GetComponentInChildren<SpriteRenderer>().color = Color.grey;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
            OnClickMiddle(true);
        else if (Input.GetMouseButtonUp(2))
            OnClickMiddle(false);

    }
    public void OnClick()
    {
        if (GetComponentInChildren<SpriteRenderer>().color == Color.grey)
        {
            entityMouse.changeUser(entityMoveMouse);
            GetComponentInParent<Toolbar>().newActive();
            GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }
    private void OnClickMiddle(bool click)
    {
        //put this button temporaly
        //this goes for both mouse listeners and button activation
        //save button and mouse listeners
        entityMouse.changeUser(entityMoveMouse, 2, click);
        GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        GetComponentInParent<Toolbar>().newActive(click);
        if (click)
            GetComponentInChildren<SpriteRenderer>().color = Color.green;
    }
}
