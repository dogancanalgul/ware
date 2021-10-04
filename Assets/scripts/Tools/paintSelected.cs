using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintSelected : MonoBehaviour, Undoable
{
    public FlexibleColorPicker fcp;

    private GameObject decolor = null;
    private Color oldColor;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void setColor()
    {
        if (null == EntityBase.current.selected)
            return;
        oldColor = EntityBase.current.selected.GetComponent<Renderer>().material.GetColor("_Color");
        EntityBase.current.selected.GetComponent<Renderer>().material.SetColor("_Color",fcp.color);
        decolor = EntityBase.current.selected;
        EntityBase.current.selectedColorChanged = true;
        UndoButton.current.Register(this);
    }
    
    public void Undo()
    {
        decolor.GetComponent<Renderer>().material.SetColor("_Color", oldColor);
    }
}
