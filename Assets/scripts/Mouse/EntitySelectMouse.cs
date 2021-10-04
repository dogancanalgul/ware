using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntitySelectMouse : MonoBehaviour, MouseUser, Undoable
{
    private Color oldColor = Color.gray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void disengage()
    {
        if (null == EntityBase.current.selected)
            return;
        //EntityBase.current.select(null);
    }

    public void keepDown()
    {
    }

    public void mouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Manipulatable")
                return;
            EntityBase.current.select(hit.collider.gameObject);
        }
        else
        {
            EntityBase.current.select(null);
        }
    }

    public void mouseUp()
    {

    }

    public void Undo()
    {
        if (null == EntityBase.current.selected)
            return;
        EntityBase.current.select(null);
    }
}
